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

const CardGrid : FC = () => {
  const { userStore } = useStores();
  return (
    <Grid>
      {
        userStore.user.posts?.length
          ? userStore.user.posts.map((post) => (
            <PhotoCard
              key={post.postId}
              description={post.description}
              src={post.photoIds}
            />
          )) : (
            <div> Постов пока нет</div>
          )
      }
    </Grid>
  );
};

export default observer(CardGrid);
