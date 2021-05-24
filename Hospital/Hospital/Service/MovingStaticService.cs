using System;
using System.Collections.Generic;
using Repository;
using Repository.Interfaces;
using Model;

namespace Service
{
    public class MovingStaticService
    {
        private readonly IMovingStaticRepository _movingStaticRepository = new MovingStaticRepository();

        public List<MovingStatic> GetAll()
        {
            return _movingStaticRepository.GetAll();

        }

        public void Save(int staticId, int roomId, DateTime dateTime)
        {
            MovingStatic movingStatic = new MovingStatic(
                GenerateNewId(),
                staticId,
                roomId,
                dateTime
                );
            _movingStaticRepository.Save(movingStatic);
        }

        public int GenerateNewId()
        {
            return _movingStaticRepository.GenerateNewId();
        }

        public void Delete(int movingId)
        {
            _movingStaticRepository.Delete(movingId);
        }

    }
}