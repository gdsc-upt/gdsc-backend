﻿using System.Collections.Generic;
using gdsc_web_backend.Models;
using gdsc_web_backend.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/settings")]
    public class SettingsController : ControllerBase
    {
        public List<SettingModel> Settings = new()
        {
            new SettingModel
            {
                Id = "1",
                Name = "some setting here",
                Slug = "some-setting",
                Type = SettingTypeEnum.Text,
                Value = true,
                Image = "probabil o sa vina o imagine aici :)"
            },
            new SettingModel
            {
                Id = "2",
                Name = "second setting here",
                Slug = "second-setting",
                Type = SettingTypeEnum.Image,
                Value = false,
                Image = "probabil o sa vina o imagine disabled aici :)"
            }
        };

        [HttpGet]
        public List<SettingModel> Get()
        {
            return Settings;
        }

        [HttpGet("{id}")]
        public SettingModel Get(string id)
        {
            return Settings.Find(x => x.Id == id);
        }

        [HttpPost]
        public ActionResult<List<SettingModel>> Post(SettingModel setting)
        {
            if (setting is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            var existing = Settings.Find(e => e.Id == setting.Id);

            if (existing != null)
            {
                return BadRequest(new ErrorViewModel {Message = "An object with the same ID already exists"});
            }

            Settings.Add(setting);
            setting = Settings.Find(example => example == setting);

            return Created("api/Examples/" + setting!.Id, setting);
        }
    }
}