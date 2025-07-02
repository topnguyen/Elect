namespace Elect.Web.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApiParameterAttribute : Attribute
    {
        /// <summary>
        /// REQUIRED. The name of the parameter. Parameter names are case sensitive.
        /// If in is "path", the name field MUST correspond to the associated path segment from the path field in the Paths Object.
        /// If in is "header" and the name field is "Accept", "Content-Type" or "Authorization", the parameter definition SHALL be ignored.
        /// For all other cases, the name corresponds to the parameter name used by the in property.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// REQUIRED. The location of the parameter.
        /// Possible values are "query", "header", "path" or "cookie".
        /// </summary>
        public string In { get; set; }
        /// <summary>
        /// A brief description of the parameter. This could contain examples of use.
        /// CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Determines whether this parameter is mandatory.
        /// If the parameter location is "path", this property is REQUIRED and its value MUST be true.
        /// Otherwise, the property MAY be included and its default value is false.
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// Sets the ability to pass empty-valued parameters.
        /// This is valid only for query parameters and allows sending a parameter with an empty value.
        /// Default value is false.
        /// If style is used, and if behavior is n/a (cannot be serialized),
        /// the value of allowEmptyValue SHALL be ignored.
        /// </summary>
        public bool AllowEmptyValue { get; set; } = false;
        /// <summary>
        /// Describes how the parameter value will be serialized depending on the type of the parameter value.
        /// Default values (based on value of in): for query - form; for path - simple; for header - simple;
        /// for cookie - form.
        /// </summary>
        public string Style { get; set; }
        public ApiParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
