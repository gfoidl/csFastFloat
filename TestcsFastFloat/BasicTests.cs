﻿using csFastFloat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Xunit;

namespace TestcsFastFloat.Tests
{
  public class ParseException : Exception
  {
    public string Value;
    public string Reason;
    private double _x;
    private double _d;

    public ParseException(string v, string reason, double x, double d)
    {
      Value = v;
      Reason = reason;
      _x = x;
      _d = d;
    }
  }

  public class BasicTests : BaseTestClass
  {
    [Fact]
    public void cas_ProfLemire()
    {
      Dictionary<string, double> sut = new Dictionary<string, double>();

      // sut.Add("1090544144181609348835077142190", 0x1.b8779f2474dfbp + 99); // TODO
      sut.Add("4503599627370496.5", 4503599627370496.5);
      sut.Add("4503599627370497.5", 4503599627370497.5);
      sut.Add("0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000044501477170144022721148195934182639518696390927032912960468522194496444440421538910330590478162701758282983178260792422137401728773891892910553144148156412434867599762821265346585071045737627442980259622449029037796981144446145705102663115100318287949527959668236039986479250965780342141637013812613333119898765515451440315261253813266652951306000184917766328660755595837392240989947807556594098101021612198814605258742579179000071675999344145086087205681577915435923018910334964869420614052182892431445797605163650903606514140377217442262561590244668525767372446430075513332450079650686719491377688478005309963967709758965844137894433796621993967316936280457084866613206797017728916080020698679408551343728867675409720757232455434770912461317493580281734466552734375", 0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000044501477170144022721148195934182639518696390927032912960468522194496444440421538910330590478162701758282983178260792422137401728773891892910553144148156412434867599762821265346585071045737627442980259622449029037796981144446145705102663115100318287949527959668236039986479250965780342141637013812613333119898765515451440315261253813266652951306000184917766328660755595837392240989947807556594098101021612198814605258742579179000071675999344145086087205681577915435923018910334964869420614052182892431445797605163650903606514140377217442262561590244668525767372446430075513332450079650686719491377688478005309963967709758965844137894433796621993967316936280457084866613206797017728916080020698679408551343728867675409720757232455434770912461317493580281734466552734375);
      sut.Add("0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000022250738585072008890245868760858598876504231122409594654935248025624400092282356951787758888037591552642309780950434312085877387158357291821993020294379224223559819827501242041788969571311791082261043971979604000454897391938079198936081525613113376149842043271751033627391549782731594143828136275113838604094249464942286316695429105080201815926642134996606517803095075913058719846423906068637102005108723282784678843631944515866135041223479014792369585208321597621066375401613736583044193603714778355306682834535634005074073040135602968046375918583163124224521599262546494300836851861719422417646455137135420132217031370496583210154654068035397417906022589503023501937519773030945763173210852507299305089761582519159720757232455434770912461317493580281734466552734375", 0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000022250738585072008890245868760858598876504231122409594654935248025624400092282356951787758888037591552642309780950434312085877387158357291821993020294379224223559819827501242041788969571311791082261043971979604000454897391938079198936081525613113376149842043271751033627391549782731594143828136275113838604094249464942286316695429105080201815926642134996606517803095075913058719846423906068637102005108723282784678843631944515866135041223479014792369585208321597621066375401613736583044193603714778355306682834535634005074073040135602968046375918583163124224521599262546494300836851861719422417646455137135420132217031370496583210154654068035397417906022589503023501937519773030945763173210852507299305089761582519159720757232455434770912461317493580281734466552734375);

      StringBuilder sb = new StringBuilder();

      foreach (KeyValuePair<string, double> kv in sut)
      {
        sb.AppendLine($"Valeur   : {kv.Key} ");
        sb.AppendLine($"Expected : {kv.Value} ");
        sb.AppendLine($"Resultat : { FastParser.ParseDouble(kv.Key)}");
      }

      ApprovalTests.Approvals.Verify(sb.ToString());
    }

    [Fact]
    private void issue13()
    {
      double? x = FastParser.ParseDouble("0");
      Assert.True(x.HasValue, "Parsed");
      Assert.True(x == 0, "Maps to 0");
    }

    [Fact]
    private void issue40()
    {
      //https://tools.ietf.org/html/rfc7159
      // A fraction part is a decimal point followed by one or more digits.
      Assert.Throws<System.FormatException>(() => Double.Parse("0."));
    }

    [Fact]
    private void issue32()
    {
      double? x = FastParser.ParseDouble("-0");
      Assert.True(x.HasValue, "could not parse -zero.");
      Assert.True(x == 0, "-zero does not map to zero.");
    }

    [Fact]
    private void issue23()
    {
      double? x = FastParser.ParseDouble("0e+42949672970");

      Assert.True(x.HasValue, "could not parse zero.");
      Assert.True(x == 0, "zero does not map to zero.");
    }

    [Fact]
    private void issue23_2()
    {
      double? x = FastParser.ParseDouble("5e0012");

      Assert.True(x.HasValue, "could not parse 5e0012.");
      Assert.True(x == 5e12, "does not map to 5e0012.");
    }

    [InlineData(0, 63)]
    [InlineData(1, 62)]
    [InlineData(2, 61)]
    [InlineData(61, 2)]
    [InlineData(62, 1)]
    [InlineData(63, 0)]
    [Theory]
    private void LeadingZeros_asExpected(int shift, int val)
    {
      ulong bit = 1;
      Assert.Equal(BitOperations.LeadingZeroCount(bit << shift), val);
    }

    [InlineData(1u << 0, 1u << 0, 1u, 0u)]
    [InlineData(1u << 0, 1u << 63, 1u << 63, 0u)]
    [InlineData(1u << 1, 1u << 63, 0u, 1u)]
    [InlineData(1u << 63, 1u << 0, 1u << 63, 0u)]
    [InlineData(1u << 63, 1u << 1, 0u, 1u)]
    [InlineData(1u << 63, 1u << 2, 0u, 2u)]
    [InlineData(1u << 63, 1u << 63, 0u, 1u << 62)]
    [Theory(Skip = "Inconclusive")]
    private void FullMultiplication_Works(ulong lhs, ulong rhs, ulong expected_low, ulong expected_high)
    {
      // Inconclusive :(
      value128 res = Utils.FullMultiplication(lhs, rhs);
      Assert.Equal(expected_low, res.low);
      Assert.Equal(expected_high, res.high);
    }

    [Fact]
    private void Issue8()
    {
      string sut = @"3."
                       + "141592653589793238462643383279502884197169399375105820974944592307816406"
                       + "286208998628034825342117067982148086513282306647093844609550582231725359"
                       + "408128481117450284102701938521105559644622948954930381964428810975665933"
                       + "446128475648233786783165271201909145648566923460348610454326648213393607"
                       + "260249141273724587006606315588174881520920962829254091715364367892590360"
                       + "011330530548820466521384146951941511609433057270365759591953092186117381"
                       + "932611793105118548074462379962749567351885752724891227938183011949129833"
                       + "673362440656643086021394946395224737190702179860943702770539217176293176"
                       + "752384674818467669405132000568127145263560827785771342757789609173637178"
                       + "721468440901224953430146549585371050792279689258923542019956112129021960"
                       + "864034418159813629774771309960518707211349999998372978";

      for (int i = 0; i != 16; i++)
      {
        double d = FastParser.ParseDouble(sut.Substring(0, sut.Length - i));
        Assert.Equal(Math.PI, d);
      }
    }

    [Fact(Skip = "Inconclusive")]
    private void Issue19()
    {
      string sut = @"3.14e";

      for (int i = 0; i != 16; i++)
      {
        double d = FastParser.ParseDouble(sut.Substring(0, sut.Length - i));
        Assert.Equal(Math.PI, d);
      }
    }

    [Fact]
    private void ScientificFails_when_InconsistentInput()
    {
      Assert.Throws<System.ArgumentException>(() => FastParser.ParseDouble("3.14", csFastFloat.Enums.chars_format.is_scientific));
    }

    [Fact]
    private void ScientificWorks_when_ConsistentInput()
    {
      Assert.Equal(3.14e10, FastParser.ParseDouble("3.14e10", csFastFloat.Enums.chars_format.is_scientific));
    }

    [Fact]
    private void FixedWorks_when_ConsistentInput()
    {
      Assert.Equal(3.14, FastParser.ParseDouble("3.14e10", csFastFloat.Enums.chars_format.is_fixed));
    }

    //    static const double testing_power_of_ten[] = {
    //      1e-307, 1e-306, 1e-305, 1e-304, 1e-303, 1e-302, 1e-301, 1e-300, 1e-299,
    //      1e-298, 1e-297, 1e-296, 1e-295, 1e-294, 1e-293, 1e-292, 1e-291, 1e-290,
    //      1e-289, 1e-288, 1e-287, 1e-286, 1e-285, 1e-284, 1e-283, 1e-282, 1e-281,
    //      1e-280, 1e-279, 1e-278, 1e-277, 1e-276, 1e-275, 1e-274, 1e-273, 1e-272,
    //      1e-271, 1e-270, 1e-269, 1e-268, 1e-267, 1e-266, 1e-265, 1e-264, 1e-263,
    //      1e-262, 1e-261, 1e-260, 1e-259, 1e-258, 1e-257, 1e-256, 1e-255, 1e-254,
    //      1e-253, 1e-252, 1e-251, 1e-250, 1e-249, 1e-248, 1e-247, 1e-246, 1e-245,
    //      1e-244, 1e-243, 1e-242, 1e-241, 1e-240, 1e-239, 1e-238, 1e-237, 1e-236,
    //      1e-235, 1e-234, 1e-233, 1e-232, 1e-231, 1e-230, 1e-229, 1e-228, 1e-227,
    //      1e-226, 1e-225, 1e-224, 1e-223, 1e-222, 1e-221, 1e-220, 1e-219, 1e-218,
    //      1e-217, 1e-216, 1e-215, 1e-214, 1e-213, 1e-212, 1e-211, 1e-210, 1e-209,
    //      1e-208, 1e-207, 1e-206, 1e-205, 1e-204, 1e-203, 1e-202, 1e-201, 1e-200,
    //      1e-199, 1e-198, 1e-197, 1e-196, 1e-195, 1e-194, 1e-193, 1e-192, 1e-191,
    //      1e-190, 1e-189, 1e-188, 1e-187, 1e-186, 1e-185, 1e-184, 1e-183, 1e-182,
    //      1e-181, 1e-180, 1e-179, 1e-178, 1e-177, 1e-176, 1e-175, 1e-174, 1e-173,
    //      1e-172, 1e-171, 1e-170, 1e-169, 1e-168, 1e-167, 1e-166, 1e-165, 1e-164,
    //      1e-163, 1e-162, 1e-161, 1e-160, 1e-159, 1e-158, 1e-157, 1e-156, 1e-155,
    //      1e-154, 1e-153, 1e-152, 1e-151, 1e-150, 1e-149, 1e-148, 1e-147, 1e-146,
    //      1e-145, 1e-144, 1e-143, 1e-142, 1e-141, 1e-140, 1e-139, 1e-138, 1e-137,
    //      1e-136, 1e-135, 1e-134, 1e-133, 1e-132, 1e-131, 1e-130, 1e-129, 1e-128,
    //      1e-127, 1e-126, 1e-125, 1e-124, 1e-123, 1e-122, 1e-121, 1e-120, 1e-119,
    //      1e-118, 1e-117, 1e-116, 1e-115, 1e-114, 1e-113, 1e-112, 1e-111, 1e-110,
    //      1e-109, 1e-108, 1e-107, 1e-106, 1e-105, 1e-104, 1e-103, 1e-102, 1e-101,
    //      1e-100, 1e-99,  1e-98,  1e-97,  1e-96,  1e-95,  1e-94,  1e-93,  1e-92,
    //      1e-91,  1e-90,  1e-89,  1e-88,  1e-87,  1e-86,  1e-85,  1e-84,  1e-83,
    //      1e-82,  1e-81,  1e-80,  1e-79,  1e-78,  1e-77,  1e-76,  1e-75,  1e-74,
    //      1e-73,  1e-72,  1e-71,  1e-70,  1e-69,  1e-68,  1e-67,  1e-66,  1e-65,
    //      1e-64,  1e-63,  1e-62,  1e-61,  1e-60,  1e-59,  1e-58,  1e-57,  1e-56,
    //      1e-55,  1e-54,  1e-53,  1e-52,  1e-51,  1e-50,  1e-49,  1e-48,  1e-47,
    //      1e-46,  1e-45,  1e-44,  1e-43,  1e-42,  1e-41,  1e-40,  1e-39,  1e-38,
    //      1e-37,  1e-36,  1e-35,  1e-34,  1e-33,  1e-32,  1e-31,  1e-30,  1e-29,
    //      1e-28,  1e-27,  1e-26,  1e-25,  1e-24,  1e-23,  1e-22,  1e-21,  1e-20,
    //      1e-19,  1e-18,  1e-17,  1e-16,  1e-15,  1e-14,  1e-13,  1e-12,  1e-11,
    //      1e-10,  1e-9,   1e-8,   1e-7,   1e-6,   1e-5,   1e-4,   1e-3,   1e-2,
    //      1e-1,   1e0,    1e1,    1e2,    1e3,    1e4,    1e5,    1e6,    1e7,
    //      1e8,    1e9,    1e10,   1e11,   1e12,   1e13,   1e14,   1e15,   1e16,
    //      1e17,   1e18,   1e19,   1e20,   1e21,   1e22,   1e23,   1e24,   1e25,
    //      1e26,   1e27,   1e28,   1e29,   1e30,   1e31,   1e32,   1e33,   1e34,
    //      1e35,   1e36,   1e37,   1e38,   1e39,   1e40,   1e41,   1e42,   1e43,
    //      1e44,   1e45,   1e46,   1e47,   1e48,   1e49,   1e50,   1e51,   1e52,
    //      1e53,   1e54,   1e55,   1e56,   1e57,   1e58,   1e59,   1e60,   1e61,
    //      1e62,   1e63,   1e64,   1e65,   1e66,   1e67,   1e68,   1e69,   1e70,
    //      1e71,   1e72,   1e73,   1e74,   1e75,   1e76,   1e77,   1e78,   1e79,
    //      1e80,   1e81,   1e82,   1e83,   1e84,   1e85,   1e86,   1e87,   1e88,
    //      1e89,   1e90,   1e91,   1e92,   1e93,   1e94,   1e95,   1e96,   1e97,
    //      1e98,   1e99,   1e100,  1e101,  1e102,  1e103,  1e104,  1e105,  1e106,
    //      1e107,  1e108,  1e109,  1e110,  1e111,  1e112,  1e113,  1e114,  1e115,
    //      1e116,  1e117,  1e118,  1e119,  1e120,  1e121,  1e122,  1e123,  1e124,
    //      1e125,  1e126,  1e127,  1e128,  1e129,  1e130,  1e131,  1e132,  1e133,
    //      1e134,  1e135,  1e136,  1e137,  1e138,  1e139,  1e140,  1e141,  1e142,
    //      1e143,  1e144,  1e145,  1e146,  1e147,  1e148,  1e149,  1e150,  1e151,
    //      1e152,  1e153,  1e154,  1e155,  1e156,  1e157,  1e158,  1e159,  1e160,
    //      1e161,  1e162,  1e163,  1e164,  1e165,  1e166,  1e167,  1e168,  1e169,
    //      1e170,  1e171,  1e172,  1e173,  1e174,  1e175,  1e176,  1e177,  1e178,
    //      1e179,  1e180,  1e181,  1e182,  1e183,  1e184,  1e185,  1e186,  1e187,
    //      1e188,  1e189,  1e190,  1e191,  1e192,  1e193,  1e194,  1e195,  1e196,
    //      1e197,  1e198,  1e199,  1e200,  1e201,  1e202,  1e203,  1e204,  1e205,
    //      1e206,  1e207,  1e208,  1e209,  1e210,  1e211,  1e212,  1e213,  1e214,
    //      1e215,  1e216,  1e217,  1e218,  1e219,  1e220,  1e221,  1e222,  1e223,
    //      1e224,  1e225,  1e226,  1e227,  1e228,  1e229,  1e230,  1e231,  1e232,
    //      1e233,  1e234,  1e235,  1e236,  1e237,  1e238,  1e239,  1e240,  1e241,
    //      1e242,  1e243,  1e244,  1e245,  1e246,  1e247,  1e248,  1e249,  1e250,
    //      1e251,  1e252,  1e253,  1e254,  1e255,  1e256,  1e257,  1e258,  1e259,
    //      1e260,  1e261,  1e262,  1e263,  1e264,  1e265,  1e266,  1e267,  1e268,
    //      1e269,  1e270,  1e271,  1e272,  1e273,  1e274,  1e275,  1e276,  1e277,
    //      1e278,  1e279,  1e280,  1e281,  1e282,  1e283,  1e284,  1e285,  1e286,
    //      1e287,  1e288,  1e289,  1e290,  1e291,  1e292,  1e293,  1e294,  1e295,
    //      1e296,  1e297,  1e298,  1e299,  1e300,  1e301,  1e302,  1e303,  1e304,
    //      1e305,  1e306,  1e307,  1e308};

    [Fact]
    private void PowersOfTen()
    {
    }

    //    TEST_CASE("powers_of_ten")
    //    {
    //      char buf[1024];
    //      WARN_MESSAGE(1e-308 == std::pow(10, -308), "On your system, the pow function is busted. Sorry about that.");
    //      bool is_pow_correct{ 1e-308 == std::pow(10, -308)};
    //      // large negative values should be zero.
    //      int start_point = is_pow_correct ? -1000 : -307;
    //      for (int i = start_point; i <= 308; ++i)
    //      {
    //        INFO("i=" << i);
    //        size_t n = size_t(snprintf(buf, sizeof(buf), "1e%d", i));
    //        REQUIRE(n < sizeof(buf)); // if false, fails the test and exits
    //        double actual;
    //        auto result = fast_float::from_chars(buf, buf + 1000, actual);
    //        CHECK_MESSAGE(result.ec == std::errc(), " I could not parse " << buf);
    //        double expected = ((i >= -307) ? testing_power_of_ten[i + 307] : std::pow(10, i));
    //        CHECK_MESSAGE(actual == expected, "String '" << buf << "'parsed to " << actual);
    //      }
    //    }

    //    template<typename T> std::string to_string(T d) {
    //      std::string s(64, '\0');
    //      auto written = std::snprintf(&s[0], s.size(), "%.*e",
    //                                   std::numeric_limits < T >::max_digits10 - 1, d);
    //      s.resize(size_t(written));
    //      return s;
    //    }

    //    template<typename T> std::string to_long_string(T d) {
    //      std::string s(4096, '\0');
    //      auto written = std::snprintf(&s[0], s.size(), "%.*e",
    //                                   std::numeric_limits < T >::max_digits10 * 10, d);
    //      s.resize(size_t(written));
    //      return s;
    //    }

    //    uint32_t get_mantissa(float f)
    //    {
    //      uint32_t m;
    //      memcpy(&m, &f, sizeof(f));
    //      return (m & ((uint32_t(1) << 23) - 1));
    //    }

    //    uint64_t get_mantissa(double f)
    //    {
    //      uint64_t m;
    //      memcpy(&m, &f, sizeof(f));
    //      return (m & ((uint64_t(1) << 57) - 1));
    //    }

    //    std::string append_zeros(std::string str, size_t number_of_zeros)
    //    {
    //      std::string answer(str);
    //      for (size_t i = 0; i < number_of_zeros; i++) { answer += "0"; }
    //      return answer;
    //    }

    //    template<class T>
    //void basic_test(std::string str, T expected)
    //    {
    //      T actual;
    //      auto result = fast_float::from_chars(str.data(), str.data() + str.size(), actual);
    //      INFO("str=" << str << "\n"
    //           << "  expected=" << fHexAndDec(expected) << "\n"
    //           << "  ..actual=" << fHexAndDec(actual) << "\n"
    //           << "  expected mantissa=" << iHexAndDec(get_mantissa(expected)) << "\n"
    //           << "  ..actual mantissa=" << iHexAndDec(get_mantissa(actual)));
    //      CHECK_EQ(result.ec, std::errc());
    //      CHECK_EQ(copysign(1, actual), copysign(1, expected));
    //      CHECK_EQ(std::isnan(actual), std::isnan(expected));
    //      CHECK_EQ(actual, expected);
    //    }

    //    void basic_test(float val)
    //    {
    //      {
    //        std::string long_vals = to_long_string(val);
    //        INFO("long vals: " << long_vals);
    //        basic_test<float>(long_vals, val);
    //      }
    //      {
    //        std::string vals = to_string(val);
    //        INFO("vals: " << vals);
    //        basic_test<float>(vals, val);
    //      }
    //    }

    //#define verify(lhs, rhs) { INFO(lhs); basic_test(lhs, rhs); }
    //#define verify32(val) { INFO(#val); basic_test(val); }

    [InlineData("INF", double.PositiveInfinity)]
    [InlineData("-INF", double.NegativeInfinity)]
    [InlineData("INFINITY", double.PositiveInfinity)]
    [InlineData("-INFINITY", double.NegativeInfinity)]
    [InlineData("infinity", double.PositiveInfinity)]
    [InlineData("-infinity", double.NegativeInfinity)]
    [InlineData("inf", double.PositiveInfinity)]
    [InlineData("-inf", double.NegativeInfinity)]
    [InlineData("1234456789012345678901234567890e9999999999999999999999999999", double.PositiveInfinity)]
    [InlineData("-2139879401095466344511101915470454744.9813888656856943E+272", double.NegativeInfinity)]
    [InlineData("1.8e308", double.PositiveInfinity)]
    [InlineData("1.832312213213213232132132143451234453123412321321312e308", double.PositiveInfinity)]
    [InlineData("2e30000000000000000", double.PositiveInfinity)]
    [InlineData("2e3000", double.PositiveInfinity)]
    [InlineData("1.9e308", double.PositiveInfinity)]
    [Theory]
    private void TestInfinity_Double(string sut, double expected_value)
    {
      Assert.Equal(expected_value, FastParser.ParseDouble(sut));
    }

    // [InlineData("9007199254740993.0", "0x1p+53")]
    // [InlineData("9007199254740993.0" + new string('0', 1000), 0x1F + 53)]
    //[InlineData("10000000000000000000", 0x1.158e460913dp + 63)]
    //[InlineData("10000000000000000000000000000001000000000000", 0x1.cb2d6f618c879p + 142)]
    //[InlineData("10000000000000000000000000000000000000000001", 0x1.cb2d6f618c879p + 142)]
    [InlineData("1.1920928955078125e-07", 1.1920928955078125e-07)]
    //[InlineData("9355950000000000000.00000000000000000000000000000000001844674407370955161600000184467440737095516161844674407370955161407370955161618446744073709551616000184467440737095516166000001844674407370955161618446744073709551614073709551616184467440737095516160001844674407370955161601844674407370955674451616184467440737095516140737095516161844674407370955161600018446744073709551616018446744073709551611616000184467440737095001844674407370955161600184467440737095516160018446744073709551168164467440737095516160001844073709551616018446744073709551616184467440737095516160001844674407536910751601611616000184467440737095001844674407370955161600184467440737095516160018446744073709551616184467440737095516160001844955161618446744073709551616000184467440753691075160018446744073709", 0x1.03ae05e8fca1cp + 63)]
    [InlineData("-0", -0.0)]
    //[InlineData("2.22507385850720212418870147920222032907240528279439037814303133837435107319244194686754406432563881851382188218502438069999947733013005649884107791928741341929297200970481951993067993290969042784064731682041565926728632933630474670123316852983422152744517260835859654566319282835244787787799894310779783833699159288594555213714181128458251145584319223079897504395086859412457230891738946169368372321191373658977977723286698840356390251044443035457396733706583981055420456693824658413747607155981176573877626747665912387199931904006317334709003012790188175203447190250028061277777916798391090578584006464715943810511489154282775041174682194133952466682503431306181587829379004205392375072083366693241580002758391118854188641513168478436313080237596295773983001708984375e-308", 0x1.0000000000002p - 1022)]
    [InlineData("1.0000000000000006661338147750939242541790008544921875", 1.0000000000000007)]
    //[InlineData("1090544144181609348835077142190", 0x1.b8779f2474dfbp + 99)]
    [InlineData("2.2250738585072013e-308", 2.2250738585072013e-308)]
    [InlineData("-92666518056446206563E3", -92666518056446206563E3)]
    [InlineData("-42823146028335318693e-128", -42823146028335318693e-128)]
    [InlineData("90054602635948575728E72", 90054602635948575728E72)]
    [InlineData("1.00000000000000188558920870223463870174566020691753515394643550663070558368373221972569761144603605635692374830246134201063722058e-309", 1.00000000000000188558920870223463870174566020691753515394643550663070558368373221972569761144603605635692374830246134201063722058e-309)]
    [InlineData("0e9999999999999999999999999999", 0.0)]
    [InlineData("-2402844368454405395.2", -2402844368454405395.2)]
    [InlineData("2402844368454405395.2", 2402844368454405395.2)]
    [InlineData("7.0420557077594588669468784357561207962098443483187940792729600000e+59", 7.0420557077594588669468784357561207962098443483187940792729600000e+59)]
    [InlineData("-1.7339253062092163730578609458683877051596800000000000000000000000e+42", -1.7339253062092163730578609458683877051596800000000000000000000000e+42)]
    [InlineData("-2.0972622234386619214559824785284023792871122537545728000000000000e+52", -2.0972622234386619214559824785284023792871122537545728000000000000e+52)]
    [InlineData("-1.0001803374372191849407179462120053338028379051879898808320000000e+57", -1.0001803374372191849407179462120053338028379051879898808320000000e+57)]
    [InlineData("-1.8607245283054342363818436991534856973992070520151142825984000000e+58", -1.8607245283054342363818436991534856973992070520151142825984000000e+58)]
    [InlineData("-1.9189205311132686907264385602245237137907390376574976000000000000e+52", -1.9189205311132686907264385602245237137907390376574976000000000000e+52)]
    [InlineData("-2.8184483231688951563253238886553506793085187889855201280000000000e+54", -2.8184483231688951563253238886553506793085187889855201280000000000e+54)]
    [InlineData("-1.7664960224650106892054063261344555646357024359107788800000000000e+53", -1.7664960224650106892054063261344555646357024359107788800000000000e+53)]
    [InlineData("-2.1470977154320536489471030463761883783915110400000000000000000000e+45", -2.1470977154320536489471030463761883783915110400000000000000000000e+45)]
    [InlineData("-4.4900312744003159009338275160799498340862630046359789166919680000e+61", -4.4900312744003159009338275160799498340862630046359789166919680000e+61)]
    [InlineData("+1", 1.0)]
    [InlineData("1.797693134862315700000000000000001e308", 1.7976931348623157e308)]
    // [InlineData("3e-324", 0x0.0000000000001F - 1022)]
    //[InlineData("1.00000006e+09", 0x1.dcd651ep + 29)]
    //[InlineData("4.9406564584124653e-324", 0x0.0000000000001p - 1022)]
    //[InlineData("4.9406564584124654e-324", 0x0.0000000000001p - 1022)]
    //[InlineData("2.2250738585072009e-308", 0x0.fffffffffffffp - 1022)]
    //[InlineData("2.2250738585072014e-308", 0x1p - 1022)]
    //[InlineData("1.7976931348623157e308", 0x1.fffffffffffffp + 1023)]
    //[InlineData("1.7976931348623158e308", 0x1.fffffffffffffp + 1023)]
    [InlineData("4503599627370496.5", 4503599627370496.5)]
    [InlineData("4503599627475352.5", 4503599627475352.5)]
    [InlineData("4503599627475353.5", 4503599627475353.5)]
    [InlineData("2251799813685248.25", 2251799813685248.25)]
    [InlineData("1125899906842624.125", 1125899906842624.125)]
    [InlineData("1125899906842901.875", 1125899906842901.875)]
    [InlineData("2251799813685803.75", 2251799813685803.75)]
    [InlineData("4503599627370497.5", 4503599627370497.5)]
    [InlineData("45035996.273704995", 45035996.273704995)]
    [InlineData("45035996.273704985", 45035996.273704985)]
    [InlineData("0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000044501477170144022721148195934182639518696390927032912960468522194496444440421538910330590478162701758282983178260792422137401728773891892910553144148156412434867599762821265346585071045737627442980259622449029037796981144446145705102663115100318287949527959668236039986479250965780342141637013812613333119898765515451440315261253813266652951306000184917766328660755595837392240989947807556594098101021612198814605258742579179000071675999344145086087205681577915435923018910334964869420614052182892431445797605163650903606514140377217442262561590244668525767372446430075513332450079650686719491377688478005309963967709758965844137894433796621993967316936280457084866613206797017728916080020698679408551343728867675409720757232455434770912461317493580281734466552734375", 0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000044501477170144022721148195934182639518696390927032912960468522194496444440421538910330590478162701758282983178260792422137401728773891892910553144148156412434867599762821265346585071045737627442980259622449029037796981144446145705102663115100318287949527959668236039986479250965780342141637013812613333119898765515451440315261253813266652951306000184917766328660755595837392240989947807556594098101021612198814605258742579179000071675999344145086087205681577915435923018910334964869420614052182892431445797605163650903606514140377217442262561590244668525767372446430075513332450079650686719491377688478005309963967709758965844137894433796621993967316936280457084866613206797017728916080020698679408551343728867675409720757232455434770912461317493580281734466552734375)]
    [InlineData("0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000022250738585072008890245868760858598876504231122409594654935248025624400092282356951787758888037591552642309780950434312085877387158357291821993020294379224223559819827501242041788969571311791082261043971979604000454897391938079198936081525613113376149842043271751033627391549782731594143828136275113838604094249464942286316695429105080201815926642134996606517803095075913058719846423906068637102005108723282784678843631944515866135041223479014792369585208321597621066375401613736583044193603714778355306682834535634005074073040135602968046375918583163124224521599262546494300836851861719422417646455137135420132217031370496583210154654068035397417906022589503023501937519773030945763173210852507299305089761582519159720757232455434770912461317493580281734466552734375", 0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000022250738585072008890245868760858598876504231122409594654935248025624400092282356951787758888037591552642309780950434312085877387158357291821993020294379224223559819827501242041788969571311791082261043971979604000454897391938079198936081525613113376149842043271751033627391549782731594143828136275113838604094249464942286316695429105080201815926642134996606517803095075913058719846423906068637102005108723282784678843631944515866135041223479014792369585208321597621066375401613736583044193603714778355306682834535634005074073040135602968046375918583163124224521599262546494300836851861719422417646455137135420132217031370496583210154654068035397417906022589503023501937519773030945763173210852507299305089761582519159720757232455434770912461317493580281734466552734375)]
    [InlineData("1438456663141390273526118207642235581183227845246331231162636653790368152091394196930365828634687637948157940776599182791387527135353034738357134110310609455693900824193549772792016543182680519740580354365467985440183598701312257624545562331397018329928613196125590274187720073914818062530830316533158098624984118889298281371812288789537310599037529113415438738954894752124724983067241108764488346454376699018673078404751121414804937224240805993123816932326223683090770561597570457793932985826162604255884529134126396282202126526253389383421806727954588525596114379801269094096329805054803089299736996870951258573010877404407451953846698609198213926882692078557033228265259305481198526059813164469187586693257335779522020407645498684263339921905227556616698129967412891282231685504660671277927198290009824680186319750978665734576683784255802269708917361719466043175201158849097881370477111850171579869056016061666173029059588433776015644439705050377554277696143928278093453792803846252715966016733222646442382892123940052441346822429721593884378212558701004356924243030059517489346646577724622498919752597382095222500311124181823512251071356181769376577651390028297796156208815375089159128394945710515861334486267101797497111125909272505194792870889617179758703442608016143343262159998149700606597792535574457560429226974273443630323818747730771316763398572110874959981923732463076884528677392654150010269822239401993427482376513231389212353583573566376915572650916866553612366187378959554983566712767093372906030188976220169058025354973622211666504549316958271880975697143546564469806791358707318873075708383345004090151974068325838177531266954177406661392229801349994695941509935655355652985723782153570084089560139142231.738475042362596875449154552392299548947138162081694168675340677843807613129780449323363759027012972466987370921816813162658754726545121090545507240267000456594786540949605260722461937870630634874991729398208026467698131898691830012167897399682179601734569071423681e-733", double.PositiveInfinity)]
    [Theory]
    private void TestGeneral_Double(string sut, double expected_value) => Assert.Equal(expected_value, FastParser.ParseDouble(sut));

    //// [InlineData("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", "0x1.2ced3p+0f")]
    // //[InlineData("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125e-38", 0x1.fffff8p - 127f)]
    // //[InlineData(append_zeros("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", 655), 0x1.2ced3p + 0f)]
    // //[InlineData(append_zeros("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", 656), 0x1.2ced3p + 0f)]
    // //[InlineData(append_zeros("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", 1000), 0x1.2ced3p + 0f)]
    // //[InlineData(append_zeros("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", 655) + "e-38", 0x1.fffff8p - 127f)]
    // //[InlineData(append_zeros("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", 656) + "e-38", 0x1.fffff8p - 127f)]
    // //[InlineData(append_zeros("1.1754941406275178592461758986628081843312458647327962400313859427181746759860647699724722770042717456817626953125", 1000) + "e-38", 0x1.fffff8p - 127f)]
    // //[InlineData("1090544144181609348835077142190", 0x1.b877ap + 99f)]
    // //[InlineData("7.5464513301849365", 0x1.e2f90ep + 2f)]
    // //[InlineData("1.1877630352973938", 0x1.30113ep + 0f)]
    // //[InlineData("0.30531780421733856", 0x1.38a53ap - 2f)]
    // //[InlineData("0.21791061013936996", 0x1.be47eap - 3f)]
    // //[InlineData("0.09289376810193062", 0x1.7c7e2ep - 4f)]
    // //[InlineData("0.012114629615098238", 0x1.8cf8e2p - 7f)]
    // //[InlineData("0.004221370676532388", 0x1.14a6dap - 8f)]
    // //[InlineData("0.0015924838953651488", 0x1.a175cap - 10f)]
    // //[InlineData("0.00036393293703440577", 0x1.7d9c82p - 12f)]
    // //[InlineData("1.1754947011469036e-38", 0x1.000006p - 126f)]
    // //[InlineData("7.0064923216240854e-46", 0x1p - 149f)]
    // //[InlineData("3.4028234664e38", 0x1.fffffep + 127f)]
    // //[InlineData("3.4028234665e38", 0x1.fffffep + 127f)]
    // //[InlineData("3.4028234666e38", 0x1.fffffep + 127f)]

    //verify32(1.00000006e+09f)]
    //verify32(1.4012984643e-45f)]
    //verify32(1.1754942107e-38f)]
    //verify32(1.1754943508e-45f)]
    // [Theory]
    // private void TestGeneral_Float_HexStrings(string sut, string expected_value)
    // {
    //  // float v = double.ToString(  0x1_2ced3_+0f;

    //   string hexString = expected_value;
    //   uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

    //   byte[] floatVals = BitConverter.GetBytes(num);
    //   float f = BitConverter.ToSingle(floatVals, 0);

    //   Assert.Equal(f, FastParser.ParseFloat(sut));
    // }
    [InlineData("-0", -0.0f)]
    [InlineData("1.1754943508e-38", 1.1754943508e-38f)]
    [InlineData("30219.0830078125", 30219.0830078125f)]
    [InlineData("16252921.5", 16252921.5f)]
    [InlineData("5322519.25", 5322519.25f)]
    [InlineData("3900245.875", 3900245.875f)]
    [InlineData("1510988.3125", 1510988.3125f)]
    [InlineData("782262.28125", 782262.28125f)]
    [InlineData("328381.484375", 328381.484375f)]
    [InlineData("156782.0703125", 156782.0703125f)]
    [InlineData("85003.24609375", 85003.24609375f)]
    [InlineData("43827.048828125", 43827.048828125f)]
    [InlineData("17419.6494140625", 17419.6494140625f)]
    [InlineData("15498.36376953125", 15498.36376953125f)]
    [InlineData("6318.580322265625", 6318.580322265625f)]
    [InlineData("2525.2840576171875", 2525.2840576171875f)]
    [InlineData("1370.9265747070312", 1370.9265747070312f)]
    [InlineData("936.3702087402344", 936.3702087402344f)]
    [InlineData("411.88682556152344", 411.88682556152344f)]
    [InlineData("206.50310516357422", 206.50310516357422f)]
    [InlineData("124.16878890991211", 124.16878890991211f)]
    [InlineData("50.811574935913086", 50.811574935913086f)]
    [InlineData("17.486443519592285", 17.486443519592285f)]
    [InlineData("13.91745138168335", 13.91745138168335f)]
    [InlineData("2.687217116355896", 2.687217116355896f)]
    [InlineData("0.7622503340244293", 0.7622503340244293f)]
    [InlineData("0.000000000000000000000000000000000000011754943508222875079687365372222456778186655567720875215087517062784172594547271728515625", 0.000000000000000000000000000000000000011754943508222875079687365372222456778186655567720875215087517062784172594547271728515625)]
    [InlineData("0.03706067614257336", 0.03706067614257336f)]
    [InlineData("0.028068351559340954", 0.028068351559340954f)]
    [InlineData("0.002153817447833717", 0.002153817447833717f)]
    [InlineData("0.0008602388261351734", 0.0008602388261351734f)]
    [InlineData("0.00013746770127909258", 0.00013746770127909258f)]
    [InlineData("16407.9462890625", 16407.9462890625f)]
    [InlineData("8388614.5", 8388614.5f)]
    [InlineData("0e9999999999999999999999999999", 0f)]
    [InlineData("4.7019774032891500318749461488889827112746622270883500860350068251e-38", 4.7019774032891500318749461488889827112746622270883500860350068251e-38f)]
    [InlineData("3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679", 3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679f)]
    [InlineData("2.3509887016445750159374730744444913556373311135441750430175034126e-38", 2.3509887016445750159374730744444913556373311135441750430175034126e-38f)]
    [InlineData("+1", 1f)]
    [InlineData("7.0060e-46", 0f)]
    [InlineData("0.00000000000000000000000000000000000000000000140129846432481707092372958328991613128026194187651577175706828388979108268586060148663818836212158203125", 0.00000000000000000000000000000000000000000000140129846432481707092372958328991613128026194187651577175706828388979108268586060148663818836212158203125f)]
    [InlineData("0.00000000000000000000000000000000000002350988561514728583455765982071533026645717985517980855365926236850006129930346077117064851336181163787841796875", 0.00000000000000000000000000000000000002350988561514728583455765982071533026645717985517980855365926236850006129930346077117064851336181163787841796875f)]
    [InlineData("0.00000000000000000000000000000000000001175494210692441075487029444849287348827052428745893333857174530571588870475618904265502351336181163787841796875", 0.00000000000000000000000000000000000001175494210692441075487029444849287348827052428745893333857174530571588870475618904265502351336181163787841796875f)]
    [Theory]
    private void TestGeneral_Float(string sut, float expected_value) => Assert.Equal(expected_value, FastParser.ParseFloat(sut));
  }
}