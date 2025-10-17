##### **List<T> 한눈에 정리**



제네릭 동적 배열(가변 길이 배열). C#에서 가장 자주 쓰이는 컨테이너 중 하나.

인덱스로 빠른 임의 접근(O(1)), 끝에 추가는 평균 O(1).

내부적으로 용량(capacity)을 관리해서 필요하면 자동으로 크기 확장.



###### 어디에 쓰는가 (대표적 사용처)



인벤토리: 추가/삭제가 잦고 순서가 의미 있을 때.

적(Enemy) 목록 관리: 씬 내 활성 적들 추적, 일괄 업데이트/제거.

오브젝트 풀링의 풀 리스트: 비활성 오브젝트 모아두고 재사용.

퀘스트 목록, 웨이포인트, 대화 리스트 등 순서 유지가 필요한 곳.

임시 수집(필터링/정렬 등) 후 처리: LINQ→List로 결과 보관.



###### 주요 연산과 시간복잡도(평균)



인덱스 접근 list\[i]	O(1)

끝에 추가 Add()	amortized O(1)

임의 위치 삽입/삭제 Insert, RemoveAt(i)	O(n) (이후 요소 시프트)

값으로 삭제 Remove(item)	O(n) (탐색 + 시프트)

탐색 Find, Contains	O(n)

정렬 Sort()	O(n log n)



###### 유의사항 \& 최적화 팁



1\) 삭제를 루프 안에서 할 때는 뒤에서부터(iterate backwards)

앞→뒤로 순회하며 RemoveAt(i) 하면 남은 요소들이 앞으로 당겨져 인덱스가 꼬임.



for (int i = enemies.Count - 1; i >= 0; i--)

{

&nbsp;   if (enemies\[i].IsDead)

&nbsp;       enemies.RemoveAt(i);

}



2\) Remove(값으로 삭제)는 탐색 포함이므로 비용이 큼

아이템이 중복 없고 ID로 빠르게 접근해야 하면 Dictionary가 낫다.



3\) 반복문 성능: for vs foreach

일반 C#에서는 foreach가 편하지만, Unity(특히 IL2CPP/.NET 버전 따라)에서 foreach가 가비지 생성을 유발할 수 있음(특히 구조체 타입/열거자).

성능 민감한 루프에선 for (int i=0; i<list.Count; i++) 권장.



4\) Capacity 관리로 재할당 줄이기

많은 요소를 추가할 예정이면 list.Capacity = expected; 또는 EnsureCapacity(n)로 미리 용량 예약.

자동 확장은 내부적으로 배열 복사를 발생시켜 비용이 듦.

myList.Capacity = 1000; // 미리 예약



5\) 대용량 파일/자주 수정되는 경우 GC·메모리 주의

Clear()는 내부 배열을 유지(용량 그대로), TrimExcess()로 용량 줄일 수 있음.

빈번한 Add/Remove가 많은 경우엔 LinkedList나 Object Pool 재검토.



6\) 동시성(스레드)

List<T>는 스레드-세이프하지 않음. 멀티스레드로 접근하면 락 필요.



7\) LINQ는 편하지만 비용 있다

간단한 필터링엔 편리하지만, 반복적 호출이나 실시간 루프엔 알맞지 않음. (중복 할당/이너레이터 할당 등)



자주 쓰는 메서드 \& 코드 예시

List<GameObject> enemies = new List<GameObject>();



// 추가

enemies.Add(newEnemy);

// 여러개 추가

enemies.AddRange(otherList);

// 인덱스 접근

var e = enemies\[0];

// 탐색 (조건)

var boss = enemies.Find(x => x.name == "Boss");

// 인덱스 찾기

int idx = enemies.FindIndex(x => x.hp <= 0);

// 삭제 (값)

enemies.Remove(someEnemy);

// 삭제 (인덱스)

enemies.RemoveAt(idx);

// 전체 초기화 (용량 유지)

enemies.Clear();

// 용량 줄이기

enemies.TrimExcess();



자주 쓰이는 예시 :  1. 적 리스트 관리, 2. 오브젝트 풀링, 3. 인벤토리 (순서 필요)

