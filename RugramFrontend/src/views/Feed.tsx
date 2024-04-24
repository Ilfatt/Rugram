import { FC, useEffect } from 'react';
import UseStores from '../hooks/useStores';
import styled from 'styled-components';
import BigPostCard from '../components/BigPostCard';
import { observer } from 'mobx-react';
import { v4 as uuidv4 } from 'uuid';

const FeedPage = styled.div`
  display: flex;
  gap: 36px;
  flex-direction: column;
  justify-content: center;
  min-height: 85vh;
  align-items: center;
  width: 90vw;
`

const FeedColumn = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 30vw;
  align-self: center;
  height: 100%;
  gap: 36px;
`


const Feed : FC = () => {
  const { feedStore, userStore } = UseStores()
  const pageNumber = 0;

  useEffect(() => {
    if (userStore.user.id) {
      feedStore.getPosts(20, pageNumber)
    }
  }, [pageNumber])

  return (
    <FeedPage>
      <FeedColumn>
        { feedStore.post ?
          feedStore.post.map((post) => {
            return (
              <BigPostCard
                key={uuidv4()}
                description={post.description}
                link={post.postId}
                profileId={post.profileId}
                src={post.photoUrls!}
              />
            )
          }) : (<>Тут пока пусто</>)
        }
      </FeedColumn>
    </FeedPage>
  )
};

export default observer(Feed);
