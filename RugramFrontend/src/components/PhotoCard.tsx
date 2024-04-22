import { FC } from "react";
import styled from "styled-components";
import { GlassDiv } from "../styles";
import Flickity from 'react-flickity-component'

import "../styles/flickity.css";
import Separator from "./ui/Separator";

const Slider = styled(Flickity)`
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  gap: 4px;
`;

const Description = styled.div`
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  width: -webkit-fill-available;
  font-size: 14px;
  font-weight: 400;
  padding: 0 12px
`;

const PostContainer = styled(GlassDiv)<{withSlider: boolean}>`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  border-radius: 12px;
  padding: 8px;
  gap: 12px;

  max-width: 20vw;
  max-height: ${(props) => props.withSlider ? "28vw" : "20vw"};

  img {
    width: 100%;
    max-width: 20vw;
    height: 100%;
    max-height: 20vw;
    border-radius: 12px;
  }
`

const PhotoCard: FC<{src: string[] | string, description?: string}> = ({src, description}) => {
  const withSlider = typeof src !== "string";
  return (
    <PostContainer
      withSlider={withSlider}
    >
      {!withSlider
        ? (
          <img src={src}/>
        ) : (
          <>
            <Slider
              static
            >
              {src.map((image) => (
                <img
                  key={image}
                  src={image}
                />
              ))}
            </Slider>
            <Separator />
            <Description>{description}</Description>
          </>

        )}
    </PostContainer>
  );
};

export default PhotoCard;
