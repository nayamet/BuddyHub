using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BOL;
using BOL.Dto;

namespace API.App_Start
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile()
        {
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<User, UserDto>();
            //});

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
            CreateMap<User, RegistrationDto>();
            CreateMap<RegistrationDto, User>();
            CreateMap<OAuthDto, OAuth>();
            CreateMap<OAuth, OAuthDto>();
            CreateMap<OAuthDto, User>();
            CreateMap<User, OAuthDto>();
            CreateMap<Log, LogDto>();
            CreateMap<LogDto, Log>();
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Like, LikeDto>();
            CreateMap<LikeDto, Like>();

            CreateMap<BOL.Profile, ProfileDto>();
            CreateMap<ProfileDto, BOL.Profile>();

            CreateMap<RecoveryPassword, RecoveryPasswordDto>();
            CreateMap<RecoveryPasswordDto, RecoveryPassword>();

            CreateMap<Save, SaveDto>();
            CreateMap<SaveDto, Save>();

            CreateMap<Follower, FollowerDto>();
            CreateMap<FollowerDto, Follower>();

        }
    }
}