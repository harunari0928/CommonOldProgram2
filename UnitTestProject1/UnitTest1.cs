using System.Net.Http;
using System.Threading.Tasks;
using CommonOldProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        // APIからのレスポンスを任意の値にする場合はどうするの？
        // APIが使えなかったときはテスト動かないよね？
        // → ラッパーパターンとDI　で解決

        [TestMethod, Description("正常系")]
        public async Task APIからのレスポンスが正常なときDBへ値が保存されていること()
        {
            var setting = new CorporateSetting
            {
                Item1 = "設定情報1",
                Item2 = "設定情報2"
            };
            await setting.Save();

            var actual = new CorporateSetting();
            await actual.ReadFromDB();

            Assert.AreEqual(setting.Item1, actual.Item1);
            Assert.AreEqual(setting.Item2, actual.Item2);
        }

        [TestMethod, Description("異常系1")]
        public async Task APIからのレスポンスが正常でなかったときにHTTPRequestExceptionが発生すること()
        {
            var setting = new CorporateSetting
            {
                Item1 = "設定情報1",
                Item2 = "設定情報2"
            };

            try
            {
                await setting.Save();
                Assert.Fail("例外が吐かれていません");
            }
            catch (HttpRequestException e)
            {
                Assert.AreEqual("API連携に失敗しました", e.Message);
            }
            catch
            {
                Assert.Fail("異なる例外が発生しました");
            }
        }

        [TestMethod, Description("異常系2")]
        public async Task APIからのレスポンスが正常でなかったときDBへ値が保存されていないこと()
        {
            var setting = new CorporateSetting
            {
                Item1 = "設定情報1",
                Item2 = "設定情報2"
            };

            try
            {
                await setting.Save();
                Assert.Fail("例外が吐かれていません");
            }
            catch (HttpRequestException e)
            {
                Assert.AreEqual("API連携に失敗しました", e.Message);
            }
            catch
            {
                Assert.Fail("異なる例外が発生しました");
            }
        }
    }
}
