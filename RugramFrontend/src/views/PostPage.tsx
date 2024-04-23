import { FC } from "react";
import styled from "styled-components";
import { icons } from "../enums";
import BigPostCard from "../components/BigPostCard";
import { GlassDiv } from "../styles";

const PostPageContainer = styled.div`
  display: flex;
  gap: 36px;
  justify-content: center;
  min-height: 85vh;
  align-items: center;
  width: 90vw;
`

const PostContainer = styled.div`
  display: flex;
  width: 40vw;
  height: 80vh;
  border-radius: 16px;
`

const Comments = styled(GlassDiv)`
  display: flex;
  width: 40vw;
  height: 100%;

`

const PostPage: FC = () => {
  // const { userStore } = UseStores();
  // const { id } = useParams();
  // const currentPost = useState<Post | undefined>();

  // useEffect(() => {
  //   if (id) {
  //     userStore.getPosts(id, 0)
  //     userStore.user.posts?.find(() => {

  //     })
  //   }
  // }, [id])

  return (
    <PostPageContainer>
      <PostContainer>
        <BigPostCard
          description="111"
          src={[icons.plus]}
        />
      </PostContainer>
      <Comments>
        Коменты
      </Comments>
    </PostPageContainer>

  );
};

export default PostPage;
