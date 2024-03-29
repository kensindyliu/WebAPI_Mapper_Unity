# WebAPI_Mapper_Unity<br>
put this code in program.main methed<br>
```csharp
            // Register AutoMapper
            services.AddAutoMapper(typeof(Program).Assembly);

            //.net Core using this way 
            services.AddSingleton<IPostService, PostService>();
            services.AddSingleton<ICommentService, CommentService>();
```
then controller can use unity and maaper
```csharp
 private readonly IMapper _mapper;
 private readonly ICommentService _commentService;

 public CommentController(IMapper mapper, ICommentService commentService)
 {
     _mapper = mapper;
     _commentService = commentService;
     //_postService = container.Resolve<IPostService>();
 }
```
