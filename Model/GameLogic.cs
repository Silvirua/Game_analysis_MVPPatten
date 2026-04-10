using System;
using System.Collections.Generic;
using System.Text;

namespace Game_analysis_MVPPattern.Model
{
    public class GameLogic : IGameLogic
    {
        // 플레이어의 유료, 무료 재화, 카운트 -> 상태 데이터
        private int PlayerFreeGems = 1500;
        private int PlayerPayGems = 500;
        private int _pityCount = 0;

        public int Fgem => PlayerFreeGems;
        public int Pgem => PlayerPayGems;

        // 천장 수치
        private int _pity = 300;

        private readonly Random _random = new Random();

        // 플레이어의 재화 변경 이벤트
        public event Action<int> OnPlayerFgemChanged;
        public event Action<int> OnPlayerPgemChanged;

        // 인터페이스 이벤트
        public event Action<bool> OnGameExit;
        public event Action<string> OnDrawResultLog;

        // 1회당 몇 코스트
        private const int DrawCost = 10;


        public void DrawProcess(ActionType playerAction)
        {
            if(playerAction == ActionType.Exit)
            {
                OnGameExit?.Invoke(true);
                return;
            }

            int count = (playerAction == ActionType.Multi) ? 10 : 1;
            int totalCost = count * DrawCost;

            if (!ConsumeGem(totalCost))
            {
                OnDrawResultLog?.Invoke("\n     == 재화가 부족합니다! == ");
                return;
            }

            // 뽑기 수행
            string resultsSummary = "\n";
            for (int i =0; i< count; i++)
            {
                resultsSummary += ExecuteSingleDraw() + "\n";
            }

            resultsSummary = string.Join("\n", resultsSummary) + $"\n[현재 천장 스택: {_pityCount}]\n";
            OnDrawResultLog?.Invoke(resultsSummary);
        }

        // 캐릭터 성급 구분
        private string GetRandomCharacterName(int grade)
        {
            Rarity targetRarity = (grade == 5) ? Rarity.FiveStar : (grade == 4) ? Rarity.FourStar : Rarity.ThreeStar;

            // 등급 필터링
            var candidates = CharacterDatabase.AllCharacters.FindAll(c => c.Rarity == targetRarity);

            if(candidates.Count == 0)
            {
                return "알 수 없는 캐릭터";
            }

            // 랜덤해서 선택
            int index = _random.Next(candidates.Count);
            return candidates[index].Name;
        }


        // 뽑기
        private string ExecuteSingleDraw()
        {
            // 천장 체크를 위한 뽑기 횟수 증가
            _pityCount++;
            
            // 일반 확률주사위, 0.0~100.0 난수
            double dice = _random.NextDouble() * 100;
            Rarity resultRarity;

            // 5성 천장 체크
            if (_pityCount >= _pity)
            {
                resultRarity = Rarity.FiveStar;              
                _pityCount = 0;
            }

            // 100회, 4성 이상
            if (_pityCount % 100 == 0)
            {
                // dice < 5.0 이하이므로, 0.5 이하일 경우 5성, 0.6~4.9까지 4성
                resultRarity = (dice < 5.0) ? Rarity.FiveStar : Rarity.FourStar;
            }

            // 일반 확률 판정 -> 3성 95, 4성 4.5, 5성 0.5
            else
            {
                if (dice < 0.5) resultRarity = Rarity.FiveStar;
                else if (dice < 5.0) resultRarity = Rarity.FourStar;
                else resultRarity = Rarity.ThreeStar;
            }

            // 5성이 나오면 언제든 천장 초기화
            if (resultRarity == Rarity.FiveStar) _pityCount = 0;

            // 확률에 따라 정해진 등급에 맞는 캐릭터를 하나 가져오기
            Character picked = GetRandomCharacterFromList(resultRarity);
            
            // 결과
            return $"[{picked.RarityStars} ] {picked.Name}";
        }

        private Character GetRandomCharacterFromList(Rarity rarity)
        {
            // 캐릭터 데이터 베이스에서 필터링 해 리스트로 제작
            var candidates = CharacterDatabase.AllCharacters.FindAll(c => c.Rarity == rarity);

            // 오류 대비
            if(candidates.Count == 0)
            {
                return new Character("임시 캐릭터", rarity);
            }

            // 성급 필터링 한 것들 중 선택
            int index = _random.Next(candidates.Count);
            return candidates[index];
        }


        // 플레이어 계정에 재화가 있는지 없는지에 대한 검사 코드
        private bool ConsumeGem(int amount)
        {
            if(PlayerFreeGems+PlayerPayGems < amount)
            {
                return false;
            }
            if (PlayerFreeGems >= amount)
            {
                PlayerFreeGems -= amount;
                OnPlayerFgemChanged?.Invoke(PlayerFreeGems);
            }
            else
            {
                int remainder = amount - PlayerFreeGems;
                PlayerFreeGems = 0;
                PlayerPayGems -= remainder;
                OnPlayerFgemChanged?.Invoke(PlayerFreeGems);
                OnPlayerPgemChanged?.Invoke(PlayerPayGems);
            }
            return true;
        }
    }
}
