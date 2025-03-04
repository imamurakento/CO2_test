using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CO2.At.Src.aFunc.GoodCurrency;

namespace CO2.At.Src.Tests.Mock
{
    internal class GoodMock : IGood
    {
        private List<ECurrency> _cList = null;
        private List<EGood> _cGoods = null;


        public GoodMock()
        {
            LoadC();
        }
        public bool CreateC(ECurrency eCurrency)
        {
            throw new NotImplementedException();
        }

        public bool DeleteC(int targetID)
        {
            throw new NotImplementedException();
        }

        public List<ECurrency> LoadC()
        {
            _cList = new();

            for (int i = 0; i < 50; i++)
            {
                ECurrency currency = new();

                currency.Set(
                                i + 20000,
                                Convert.ToDateTime("2025/01/11"),
                                i + 1,
                                "ユーロ",
                                "#00:00");
                _cList.Add(currency);

            }
            return _cList;
        }

        public List<EGood> LoadG()
        {
            _cGoods = new();

            for (int i = 0; i < 50; i++)
            {
                EGood good = new();

                good.Set(
                                30000,
                                Convert.ToDateTime("2025/01/11"),
                                i + 1,
                                "排出権",
                                i + 2,
                                "ユーロ",
                                i+4,
                                i+5,
                                i+6);
                _cGoods.Add(good);

            }
            return _cGoods;
        }

        public ECurrency ReadC(int targetID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateC(ECurrency eCurrency)
        {
            throw new NotImplementedException();
        }
    }
}
