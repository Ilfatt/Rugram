import { FC } from "react";
import styled from "styled-components";

interface Props {
  postId: string;
  userId: string;
  withFooter?: boolean;
}

const PostContainer = styled.div`
  display: flex;
  flex-direction: column;
  width: 20vw;
  height: 25vh;
  border-radius: 16px;
`

const PostPage: FC<Props> = ({ postId, userId, withFooter = false }) => {
  return (
    <PostContainer>
      <img src=""/>
      {withFooter && (
        <div>
          <img src="" />
          <div>{postId + userId}</div>
        </div>
      )}
    </PostContainer>
  );
};

export default PostPage;
