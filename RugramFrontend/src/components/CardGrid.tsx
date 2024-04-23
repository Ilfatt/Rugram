import { FC } from 'react';
import useStores from '../hooks/useStores';
import styled from 'styled-components';
import { observer } from 'mobx-react';
import PhotoCard from './PhotoCard';

const Grid = styled.div`
  width: 100%;
  display: grid;
  grid-template-columns: repeat(3, 20vw);
  gap: 2vw;
  flex-wrap: wrap;
`;

const NoPosts = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 36px;
  width: 100%;
  min-width: 60vw;
`

const CardGrid : FC = () => {
  const { userStore } = useStores();

  return userStore.user.posts?.length
    ? (
      <Grid>
        {
          userStore.user.posts.map((post) => {
            if (post.photoUrls) {
              return (
                <PhotoCard
                  key={post.postId}
                  description={post.description}
                  link={post.postId}
                  src={post.photoUrls}
                />
              )
            }
          })}
      </Grid>
    ) : (
      <NoPosts> Постов пока нет</NoPosts>
    )

};

export default observer(CardGrid);
