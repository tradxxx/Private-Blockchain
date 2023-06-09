﻿namespace WEB_BlockChain.Models
{
    public class Chain
    {
        public List<Block> Blocks { get; set; }

        public Block Last { get;set; }
     
   
        public bool Check()
        {
            //Транзакция считается защищенной, если ПОСЛЕ НЕЁ ЕСТЬ ХОТЯ БЫ ОДНА СЛЕДУЮЩАЯ ТРАНЗАКЦИЯ, ЕСЛИ False, ТО НУЖНО УДАЛИТЬ ПОСЛЕДНЮЮ ТРАНЗАКЦИЮ, ТК ПРИ ЕЁ ФОРМИРОВАНИИ ПРОИЗОШЕЛ ВЗЛОМ
            for (int i = 1; i < Blocks.Count; i++)
            {
                // blockchain[2].amount = 3454353; Изменение содержимого блока на фальшивые данные
                if (Blocks[i].Hash != Blocks[i].GetHash(Blocks[i].GetData(Blocks[i].Created)))
                {
                    return false;
                }
                //blockchain[2].hash = blockchain[2].getHash(); Изменение хэша блока на фальшивый хэш
                if (Blocks[i].PreviousHash != Blocks[i - 1].Hash)
                {
                    return false;
                }
                //Проверка связи (пересчёт хэша)
                if (Blocks[i].PreviousHash != Blocks[i - 1].GetHash(Blocks[i - 1].GetData(Blocks[i - 1].Created)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
