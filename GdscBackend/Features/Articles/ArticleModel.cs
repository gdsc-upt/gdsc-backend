﻿using GdscBackend.Auth;
using GdscBackend.Common.Models;

namespace GdscBackend.Features.Articles;

public class ArticleModel : Model
{
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public User? Author { get; set; }
}