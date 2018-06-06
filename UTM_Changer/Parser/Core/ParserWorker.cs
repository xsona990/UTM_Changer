using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTM_Changer.Parser
{
    [Serializable]
    class ParserWorker<T> where T : class
    {
        #region Variables
        IParser<T> parser;
        IParserSettings parserSettings;
        string querySelector;
        string className;
        HtmlLoader loader;

        bool isActive;
        #endregion
        #region Properties
        public string ClassName { get => className; set => className = value; }
        public string QuerySelector { get => querySelector; set => querySelector = value; }
        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        #endregion
        #region Events
        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;
        #endregion
        #region Constructors
        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }
        #endregion
        #region Methods
        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseAsync(source);

                var result = parser.Parse(document, querySelector, className);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;
        }
        #endregion

    }
}
