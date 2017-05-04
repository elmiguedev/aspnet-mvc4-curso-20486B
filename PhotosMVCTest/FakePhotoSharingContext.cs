using PhotosMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosMVCTest
{
    public class FakePhotoSharingContext : IPhotoSharingContext
    {
        SetMap _map = new SetMap();

        public FakePhotoSharingContext()
        {
            var photos = new List<Photo> {

                new Photo()
                {
                    PhotoID = 1,
                    Title = "Bulbasaur",
                    Description = "Bulbasaur, el pokemon inicial de planta",
                    UserName = "Prof. Oak",
                    PhotoFile = new byte[1],
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
                    PhotoID = 2,
                    Title = "Charmander",
                    Description = "Charmander, el pokemon inicial de fuego",
                    UserName = "Prof. Oak",
                    PhotoFile = new byte[1],
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
                    PhotoID = 3,
                    Title = "Squirtle",
                    Description = "Squirtle, el pokemon inicial de agua",
                    UserName = "Prof. Oak",
                    PhotoFile = new byte[1],
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
            _map.Use<Photo>(photos);
        }

        public IQueryable<Photo> Photos
        {
            get { return _map.Get<Photo>().AsQueryable(); }
            set { _map.Use<Photo>(value); }
        }

        public IQueryable<Comment> Comments
        {
            get { return _map.Get<Comment>().AsQueryable(); }
            set { _map.Use<Comment>(value); }
        }

        public T Add<T>(T entity) where T : class
        {
            _map.Get<T>().Add(entity);
            return entity;
        }

        public T Delete<T>(T entity) where T : class
        {
            _map.Get<T>().Remove(entity);
            return entity;
        }

        public Comment FindCommentById(int ID)
        {
            Comment item = Comments.Where(x => x.CommentID == ID).FirstOrDefault();
            return item;
        }

        public Photo FindPhotoById(int ID)
        {
            Photo item = Photos.Where(x => x.PhotoID == ID).FirstOrDefault();
            return item;
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Photo FindPhotoByTitle(string title)
        {
            return _map.Get<Photo>().Where(x => x.Title == title).FirstOrDefault();
        }

        class SetMap : KeyedCollection<Type, object>
        {
            public HashSet<T> Use<T>(IEnumerable<T> sourceData)
            {
                var set = new HashSet<T>(sourceData);
                if (Contains(typeof(T)))
                {
                    Remove(typeof(T));
                }
                Add(set);
                return set;
            }

            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }
    }
}
