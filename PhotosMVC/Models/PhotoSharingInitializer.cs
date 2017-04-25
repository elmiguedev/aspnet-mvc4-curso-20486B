using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotosMVC.Models
{
    /// <summary>
    /// El objetivo de esta clase es inicializar la base de datos
    /// con datos basicos para poder trabajar
    /// </summary>
    public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingDB>
    {
        protected override void Seed(PhotoSharingDB context)
        {
            base.Seed(context);

            var photos = new List<Photo> {

                new Photo()
                {
                    Title = "Bulbasaur",
                    Description = "Bulbasaur, el pokemon inicial de planta",
                    UserName = "Prof. Oak",
                    PhotoFile = getFileBytes("\\images\\bulbasaur.png"),
                    ImageMimeType = "image/png",
                    CreatedDate = DateTime.Today,
                    Comments = new List<Comment>() {
                        new Comment(){
                            UserName = "Nico",
                            Subject = "No me gusta",
                            Body = "No me gustan los pokemon de planta"
                        },
                        new Comment(){
                            UserName = "Migue",
                            Subject = "Buena opcion",
                            Body = "Si lo usas en el primer GYM va como trompada"
                        },
                        new Comment(){
                            UserName = "Pablo",
                            Subject = "No se que elegir",
                            Body = "Elijo el primero que venga"
                        }
                    }
                },
                new Photo()
                {
                    Title = "Charmander",
                    Description = "Charmander, el pokemon inicial de fuego",
                    UserName = "Prof. Oak",
                    PhotoFile = getFileBytes("\\images\\charmander.png"),
                    ImageMimeType = "image/png",
                    CreatedDate = DateTime.Today,
                    Comments = new List<Comment>() {
                        new Comment(){
                            UserName = "Nico",
                            Subject = "Mortal",
                            Body = "Esta mortal charmander!"
                        }
                    }
                },
                new Photo()
                {
                    Title = "Squirtle",
                    Description = "Squirtle, el pokemon inicial de agua",
                    UserName = "Prof. Oak",
                    PhotoFile = getFileBytes("\\images\\squirtle.png"),
                    ImageMimeType = "image/png",
                    CreatedDate = DateTime.Today,
                    Comments = new List<Comment>() {
                        new Comment(){
                            UserName = "Nico",
                            Subject = "Se la banca",
                            Body = "Se la banca un caño contra brook!"
                        },
                        new Comment(){
                            UserName = "Migue",
                            Subject = "Aguante squirtle",
                            Body = "Cuando evoluciona a Blastoise es genial!"
                        }
                    }
                }
            };

            // agrega las fotos al modelo
            photos.ForEach(x => context.Photos.Add(x));
            context.SaveChanges();

        }

        //This gets a byte array for a file at the path specified
        //The path is relative to the route of the web site
        //It is used to seed images
        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}