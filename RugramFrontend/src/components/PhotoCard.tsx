import { FC } from "react";
import styled from "styled-components";
import { GlassDiv } from "../styles";

const PostContainer = styled(GlassDiv)`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  border-radius: 12px;
  padding: 8px;

  img {
    width: 100%;
    height: 100%;
    border-radius: 12px;
  }
`

const PhotoCard: FC<{src: string}> = ({src}) => {
  return (
    <PostContainer>
      <img src={src}/>
    </PostContainer>
  );
};

export default PhotoCard;
