using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositories {
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class {

        private readonly AppDbContext _context;

        public RepositoryGeneric(AppDbContext context) {
            _context = context;
            }



        public async Task Atualizar(TEntity entity) {
            try {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                }

            catch(Exception ex) {
                throw ex;
                }

            }

        public async Task Excluir(int id) {

            var entity = await PegarPeloId(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();


            }
        public async Task Excluir(string id) {

            var entity = await PegarPeloId(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();


            }

        public async Task Inserir(TEntity entity) {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();


            }

        public async Task<TEntity> PegarPeloId(int id) {
            return await _context.Set<TEntity>().FindAsync(id);

            }
        public async Task<TEntity> PegarPeloId(string id) {
            return await _context.Set<TEntity>().FindAsync(id);

            }


        public IQueryable<TEntity> PegarTodos() {
            return _context.Set<TEntity>();

            }
        }
    }
