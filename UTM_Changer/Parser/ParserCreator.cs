using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTM_Changer.Parser
{
    [Serializable]
    class ParserCreator
    {
        #region Variables
        private ParserWorker<ParsingResult[]> parser;
        private string baseUrl;
        private string prefix;
        private int startPoint;
        private int endPoint;
        public bool isCompleted = false;
        private List<ParsingResult> resultList;
        #endregion
        #region Properties
        public List<ParsingResult> ResultList { get => resultList; private set => resultList = value; }
        public int EndPoint { get => endPoint; set => endPoint = value; }
        #endregion
        #region Constructors
        public ParserCreator()
        {
            parser = new ParserWorker<ParsingResult[]>(new Parser());

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
            ResultList = new List<ParsingResult>();
        }
        public ParserCreator(int startPoint, int endPoint)
        {
            parser = new ParserWorker<ParsingResult[]>(new Parser());

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
            ResultList = new List<ParsingResult>();
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="className">post_title e.t.c</param>
        /// <param name="querySelector">div, a, h4 e.t.c</param>
        /// <param name="baseUrl">site url: http://site.com/ </param>
        /// <param name="prefix">page={CurrentId} or page{CurrentId}, e.t.c.</param>
        /// <param name="startPoint">start site page</param>
        /// <param name="endPoint">end site page</param>
        public ParserCreator(string className, string querySelector, string baseUrl, string prefix) : this(1,1)
        {
            parser.ClassName = className;
            parser.QuerySelector = querySelector;
            this.baseUrl = baseUrl;
            this.prefix = prefix;
        }
        #endregion
        #region Methods
        private void Parser_OnNewData(object arg1, ParsingResult[] arg2)
        {
            ResultList = arg2.ToList();
        }

        private void Parser_OnCompleted(object obj)
        {
            isCompleted = true;
        }

        public void Parse()
        {
            parser.Settings = new ParserSettings(baseUrl, prefix, startPoint, endPoint);
            parser.Start();
        }
        #endregion
    }
}
