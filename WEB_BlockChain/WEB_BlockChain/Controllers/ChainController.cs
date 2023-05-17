using Microsoft.AspNetCore.Mvc;
using WEB_BlockChain.Models;

namespace WEB_BlockChain.Controllers
{
    public class ChainController : Controller
    {
        private readonly BlockchainContext _context;

        public static Chain chain = new Chain();

        public ChainController(BlockchainContext context)
        {
            _context = context;
        }

        public IActionResult View()
        {
            var blocks = _context.Blocks;
            return View(blocks);
        }

        [HttpGet]
        public ViewResult CreateBlock()
        {      
            return View("CreateBlock");
        }


        [HttpPost]
        public IActionResult CreateBlock(Block item)
        {
            if (ModelState.IsValid)
            {
                //Синхронизация с клиентом и проверка BlockChain
                chain.Blocks = _context.Blocks.ToList<Block>();

                if (chain.Blocks.Count() == 0)
                {
                    var genesisBlock = new Block();

                    chain.Blocks.Add(genesisBlock);
                    chain.Last = genesisBlock;
                    //chain.Last.Id = null;
                    _context.Blocks.Add(chain.Last);
                    _context.SaveChanges();
                }
                else
                {
                    if (chain.Check())
                    {
                        chain.Last = chain.Blocks.Last();
                    }
                    else
                    {
                        throw new Exception("Ошибка в получении блоков из базы данных");
                    }
                }




                if (item == null)
                {
                    return BadRequest();
                }
                Block block = new Block(item.Data, item.User, chain.Last);
                chain.Last = block;
                //block.Id = null;

                _context.Blocks.Add(block);
                _context.SaveChanges();

                //var blocks = _context.Blocks;
                return View("View", _context.Blocks);
            }
            else
            {
                return View("CreateBlock");
            }
            
        }
    }
}
