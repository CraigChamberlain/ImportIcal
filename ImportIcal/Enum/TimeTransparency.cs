namespace ImportIcal.Enum
{
    /// <summary>
    /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.2.7
    /// </summary>
    public enum TimeTransparency
    { 
        TRANSPARENT,    // Transparent on busy time searches.
        OPAQUE          // Blocks or opaque on busy time searches.
                
    }
}
