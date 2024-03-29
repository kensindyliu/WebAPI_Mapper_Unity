using AutoMapper;
using Entities;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile() 
        { 
            //CreateMap<Book,BookDTO>();
            //CreateMap<BookDTO,Book>();

            CreateMap<Comment,CommentDTO>();
            CreateMap<CommentDTO, Comment>();

            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
        }
    }
}
