using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QA_Test_Log.Data;
using QA_Test_Log.Interface;

namespace QA_Test_Log.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
[EnableCors("MyPolicy")]
public abstract class GenericCRUDController<MainRepo, MainEntity, PKType> : ControllerBase
    where MainEntity : class
    where MainRepo : _IAbsGenericRepo<MainEntity, PKType>
{
    protected readonly MainRepo _mainRepo;
    protected readonly AppDbContext dbContext;

    public GenericCRUDController(MainRepo mainRepo)
    {
        _mainRepo = mainRepo;
    }

    [HttpGet]
    public virtual async Task<ActionResult<List<MainEntity>>> GetList()
    {
        var items = await _mainRepo.GetList().ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<MainEntity>> GetDetails(PKType id)
    {
        var item = await _mainRepo.GetDetails(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    protected virtual async Task<bool> OnCreated(MainEntity data)
    {
        return true;
    }

    [HttpPost]
    public virtual async Task<ActionResult<MainEntity>> Create(MainEntity data)
    {
        MainEntity createdData = await _mainRepo.Create(data);
        //trigger on Created Action
        await OnCreated(createdData);
        return Ok(createdData);

    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<MainEntity>> Update(PKType id, MainEntity updatedData)
    {
        var updatedItem = await _mainRepo.Update(id, updatedData);

        if (updatedItem == null)
        {
            return NotFound();
        }

        return Ok(updatedItem);
    }


    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<MainEntity>> Delete(PKType id)
    {
        var deletedItem = await _mainRepo.Delete(id);
        return Ok(deletedItem);
    }
}

