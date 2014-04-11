// MatchProviderStub.cs

namespace MultiLevelGeoCoderTests
{
    using MultiLevelGeoCoder.DataAccess;
    using Rhino.Mocks;

    /// <summary>
    /// Provides Match Provider stubs containing name matches
    /// </summary>
    internal class MatchProviderStub
    {
        #region Fields

        private readonly MatchProviderTestData matchProviderTestData;

        #endregion Fields

        #region Constructors

        public MatchProviderStub(MatchProviderTestData matchProviderTestData)
        {
            this.matchProviderTestData = matchProviderTestData;
            Alternate = new string[3];
            Actual = new string[3];
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the actual name to use when finding a single match.
        /// </summary>
        /// <value>
        /// The actual names.
        /// </value>
        public string[] Actual { get; set; }

        /// <summary>
        /// Gets or sets the alternate names to use when finding a single match.
        /// </summary>
        /// <value>
        /// The alternate names.
        /// </value>
        public string[] Alternate { get; set; }

        #endregion Properties

        #region Methods

        public IMatchProvider MatchProvider()
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();

            // get all matches
            matchStub.Stub(x => x.GetAllLevel1())
                .Return(matchProviderTestData.AllLevel1());
            matchStub.Stub(
                x => x.GetAllLevel2())
                .Return(matchProviderTestData.AllLevel2());
            matchStub.Stub(
                x =>
                    x.GetAllLevel3())
                .Return(matchProviderTestData.AllLevel3());

            // single level1 match
            matchStub.Stub(x => x.GetMatches(Alternate[0]))
                .Return(
                    matchProviderTestData.Level1Matches
                        (Alternate[0]));

            // single level2 match
            matchStub.Stub(
                x => x.GetMatches(
                    Alternate[1],
                    Actual[0]))
                .Return(
                    matchProviderTestData.Level2Matches(
                        Actual[0],
                        Alternate[1]));

            // single level3 match
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        Alternate[2],
                        Actual[0],
                        Actual[1]))
                .Return(
                    matchProviderTestData.Level3Matches(
                        Actual[0],
                        Actual[1],
                        Alternate[2]));

            // provide default empty lists if no records found

            //  default empty list if no level 1 match
            matchStub.Stub(x => x.GetMatches(Arg<string>.Is.Anything))
              .Return(
                   matchProviderTestData.EmptyLevel1List());

            //  default  empty list if no level 2 match
            matchStub.Stub(
                x => x.GetMatches(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything))
                .Return(
                    matchProviderTestData.EmptyLevel2List());

            // default  empty list if no level3 match
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(
                    matchProviderTestData.EmptyLevel3List());

            return matchStub;
        }

        #endregion Methods

        public static IMatchProvider EmptyStub()
        {
            return MockRepository.GenerateStub<IMatchProvider>();
        }
    }
}