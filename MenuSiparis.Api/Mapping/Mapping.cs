using AutoMapper;
using MenuSiparis.Core.Entities;
using MenuSiparis.Dto.AboutDto;
using MenuSiparis.Dto.BookingDto;
using MenuSiparis.Dto.CategoryDto;
using MenuSiparis.Dto.ContactDto;
using MenuSiparis.Dto.DiscountDto;
using MenuSiparis.Dto.FeatureDto;
using MenuSiparis.Dto.ProductDto;
using MenuSiparis.Dto.SocialMediaDto;
using MenuSiparis.Dto.TestimonialDto;


namespace SignalRApi.Mapping
{
    public class Mapping : Profile
    {
       public Mapping()
        {
           CreateMap<About,CreateAboutDto>().ReverseMap();
              CreateMap<About,GetAboutDto>().ReverseMap();
            CreateMap<About,UpdateAboutDto>().ReverseMap();
            CreateMap<About,ResultAboutDto>().ReverseMap();
            CreateMap<Booking, ResultBookingDto>().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().ReverseMap();
            CreateMap<Booking, GetBookingDto>().ReverseMap();
            CreateMap<Booking, UpdateBookingDto>().ReverseMap();
            CreateMap<Category,ResultCategoryDto>().ReverseMap();
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,GetCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryDto>().ReverseMap();
            CreateMap<Contact,ResultContactDto>().ReverseMap();   
            CreateMap<Contact,CreateContactDto>().ReverseMap();
            CreateMap<Contact,GetContactDto>().ReverseMap();
            CreateMap<Contact,UpdateContactDto>().ReverseMap();
            CreateMap<Discount,ResultDiscountDto>().ReverseMap();
            CreateMap<Discount,CreateDiscountDto>().ReverseMap();
            CreateMap<Discount,GetDiscountDto>().ReverseMap();
            CreateMap<Discount,UpdateDiscountDto>().ReverseMap();
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<SocialMedia, ResultSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, CreateSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, GetSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, UpdateSocialMediaDto>().ReverseMap();
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
        }


    }
}
