using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.DataFeeder
{
    public class CharacterRelationsMapping
    {
        private readonly IDictionary<int, List<int>> _bookMapping;
        private readonly IDictionary<int, List<int>> _povBookMapping;
        private readonly IDictionary<int, List<int>> _swornMembersMapping;

        public CharacterRelationsMapping(IDictionary<int, List<int>> bookMapping, IDictionary<int, List<int>> povBookMapping, IDictionary<int, List<int>> swornMembersMapping)
        {
            _bookMapping = bookMapping;
            _povBookMapping = povBookMapping;
            _swornMembersMapping = swornMembersMapping;
            if (bookMapping == null) throw new ArgumentNullException(nameof(bookMapping));
            if (povBookMapping == null) throw new ArgumentNullException(nameof(povBookMapping));
            if (swornMembersMapping == null) throw new ArgumentNullException(nameof(swornMembersMapping));
        }

        public List<int> GetCharacters(int bookId)
        {
            return _bookMapping.ContainsKey(bookId) ? _bookMapping[bookId] : new List<int>();
        }

        public List<int> GetPovCharacters(int bookId)
        {
            return _povBookMapping.ContainsKey(bookId) ? _povBookMapping[bookId] : new List<int>();
        }

        public List<int> GetSwornMembers(int houseId)
        {
            return _swornMembersMapping.ContainsKey(houseId) ? _swornMembersMapping[houseId] : new List<int>();
        }
    }
}