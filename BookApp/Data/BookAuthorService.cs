using BookApp.Interfaces;
using BookApp.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BookApp.Data
{
   public class BookAuthorService : IBookAuthorService
   {
      private readonly IDapperService _dapperService;

      public BookAuthorService(IDapperService dapperService)
      {
         this._dapperService = dapperService;
      }

      public Task<int> Create(BookAuthorName bookAuthorName)
      {
         var dbPara = new DynamicParameters();
         dbPara.Add("ISBN", bookAuthorName.ISBN, DbType.Int64);
         dbPara.Add("AuthorId", bookAuthorName.AuthorId,
                    DbType.Int32);
         dbPara.Add("AuthorOrd", bookAuthorName.AuthorOrd,
                    DbType.Byte);
         var bookAuthorId = Task.FromResult(_dapperService.
             Insert<int>("[dbo].[spAddBookAuthor]", dbPara,
             commandType: CommandType.StoredProcedure));
         return bookAuthorId;
      }

      public Task<int> Delete(long isbn, int authorId)
      {
         var deleteBookAuthor = Task.FromResult(_dapperService.
             Execute($"Delete [BookAuthor] where ISBN = {isbn} " +
             $"and AuthorId = {authorId}", null,
             commandType: CommandType.Text));
         return deleteBookAuthor;
      }

      public Task<List<BookAuthorName>> FetchAll(long isbn)
      {
         var bookAuthorNames = Task.FromResult(_dapperService.GetAll
             <BookAuthorName>($"select * from BookAuthorName " +
             $"({isbn}) order by AuthorName; ", null,
             commandType: CommandType.Text));
         return bookAuthorNames;
      }
      /*
      public Task<BookAuthor> ReadByPk(long isbn, int authorId)
      {
         var bookAuthor = Task.FromResult(_dapperService.
             Get<BookAuthor>($"select * from [BookAuthor] where " +
             $"ISBN = {isbn} and AuthorId = {authorId}",
             null, commandType: CommandType.Text));
         return bookAuthor;
      }
      
      public Task<int> Update(BookAuthor bookAuthor)
      {
         var dbPara = new DynamicParameters();
         dbPara.Add("ISBN", bookAuthor.ISBN, DbType.Int64);
         dbPara.Add("AuthorId", bookAuthor.AuthorId, DbType.Int32);
         dbPara.Add("AuthorOrd", bookAuthor.AuthorOrd, DbType.Byte);
         var updateBookAuthor = Task.FromResult(_dapperService.
             Update<int>("[dbo].[spUpdateBookAuthor]", dbPara,
             commandType: CommandType.StoredProcedure));
         return updateBookAuthor;
      }
      
      public Task<int> Count(long isbn)
      {
         var totBookAuthor = Task.FromResult(_dapperService.Get<int>
             ($"select COUNT(*) from [BookAuthor] WHERE ISBN = " +
             $"{isbn}", null, commandType: CommandType.Text));
         return totBookAuthor;
      }      
      
      public Task<List<BookAuthor>> ListAll(int skip, int take,
             string orderBy, string direction = "DESC")
      {
         var bookAuthors = Task.FromResult(_dapperService.GetAll
             <BookAuthor>($"SELECT * FROM [BookAuthor] " +
             $"ORDER BY {orderBy} {direction} OFFSET {skip} " +
             $"ROWS FETCH NEXT {take} ROWS ONLY; ", null, 
             commandType: CommandType.Text));
         return bookAuthors;
      }
      */
   }
}