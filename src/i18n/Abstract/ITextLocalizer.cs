﻿namespace i18n
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Defines a service for retrieving localized text from a data source.
    /// </summary>
    public interface ITextLocalizer
    {
        /// <summary>
        /// Obtains dictionary of language tags (key = langtag string, value = LanguageTag instance)
        /// describing the set of Po-valid languages (that is, the languages for which one or more 
        /// resource are defined).
        /// </summary>
        ConcurrentDictionary<string, LanguageTag> GetAppLanguages();

        /// <summary>
        /// Looks up and returns localized text for a resource.
        /// </summary>
        /// <param name="msgid">
        /// Specifies the msgid of the subject resource.
        /// Null if we are not interested in a particular resource but wish to know
        /// the best matching language for which ANY resources are available (one or more).
        /// </param>
        /// <param name="msgcomment">
        /// Specifies the optional message comment value of the subject resource, or null/empty.
        /// </param>
        /// <param name="languages">
        /// A list of language preferences, sorted in order of preference (most preferred first).
        /// </param>
        /// <param name="o_langtag">
        /// On success, outputs a description of the language from which the resource was selected.
        /// </param>
        /// <param name="maxPasses">
        /// 0 - allow exact match only
        /// 1 - allow exact match or default-region match only
        /// 2 - allow exact match or default-region match or script match only
        /// 3 - allow exact match or default-region match or script match or language match only
        /// 4 - allow exact match or default-region match or script match or language match only, or failing return the default language.
        /// -1 to set to most tolerant (i.e. 4).
        /// </param>
        /// <returns>
        /// When <paramref name="msgid"/> is set to non-null, returns either the sucessully-looked up localized string, or 
        /// null if the lookup failed. See also LocalizedApplication.MessageKeyIsValueInDefaultLanguage which when
        /// true can cause msgid to be returned instead of null.
        /// When <paramref name="msgid"/> is set to null, returns "" to indicate a match to a PO-valid language was made
        /// (PO-valid meaning that one or more messages/resources are defined for that language),
        /// or null if no match was made.
        /// </returns>
        string GetText(
            string msgid, 
            string msgcomment, 
            LanguageItem[] languages, 
            out LanguageTag o_langtag,
            int maxPasses = -1);
    }
}
