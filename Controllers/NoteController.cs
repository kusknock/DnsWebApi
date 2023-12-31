﻿using DnsWebApi.Exceptions;
using DnsWebApi.Models;
using DnsWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DnsWebApi.Controllers
{
    [ApiController]
    [Route("notes")]
    public class NoteController : ControllerBase
    {
        private INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(Note note)
        {
            try
            {
                var result = await noteService.Create(note);

                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<ActionResult> Edit(int id, string note)
        {
            try
            {
                var result = await noteService.Update(id, note);

                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await noteService.Delete(id);

                return Ok(result);
            }
            catch (ItemNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all-notes")]
        public ActionResult GetAll()
        {
            return Ok(noteService.GetAll());
        }

        [HttpGet]
        [Route("unread-notes")]
        public ActionResult GetUnread()
        {
            return Ok(noteService.GetUnread());
        }
    }
}
