import { FC, useCallback, useEffect, useState } from "react";
import styled from "styled-components";
import { GlassDiv } from "../styles";
import Flickity from 'react-flickity-component'
import { v4 as uuidv4 } from 'uuid';
import "../styles/flickity-2.css";
import Separator from "./ui/Separator";
import { observer } from "mobx-react";
import { Icon, SearchLink } from "./SearchBar";
import UseStores from "../hooks/useStores";
import { icons } from "../enums";
import { useNavigate } from "react-router-dom";

const Slider = styled(Flickity)`
  width: 100%;
  /* height: 100%; */
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
  padding: 0 12px;
`;

const PostContainer = styled(GlassDiv)`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  border-radius: 12px;
  padding: 8px;
  gap: 8px;

  width: -webkit-fill-available;
  height: -webkit-fill-available;

  cursor: pointer;

  img {
    width: 100%;
    max-width: 30vw;
    height: 100%;
    max-height: 29vw;
    border-radius: 12px;
  }
`

const PostTitle = styled.b`
  font-size: 24px;
`

const BigPostCard: FC<{
  src: string[],
  description?: string,
  profileId?: string,
}> = ({src, description, profileId}) => {
  const { userStore } = UseStores();
  const [icon, setIcon] = useState('');
  const [login, setLogin] = useState('');
  const navigate = useNavigate()

  useEffect(() => {
    const getProfile = async () => {
      if (profileId) {
        const profile = await userStore.getPostProfile(profileId);
        setLogin(profile?.profileName ?? '');
        setIcon(`data:image/png;base64, ${profile?.icon.data.profilePhoto}` ?? '');
      }

    }

    getProfile();
  }, [])

  const MemoSlider = useCallback((images : string[]) => {
    return (
      <Slider
        static
      >
        {images ? images.map((image) => (
          <img
            key={image}
            src={`data:image/png;base64, ${image}`}
          />
        )) : null}
      </Slider>
    )
  }, [src])

  return (
    <PostContainer>
      <SearchLink
        key={uuidv4()}
        onClick={() => {
          navigate(`/profile/${profileId}`)
        }}
      >
        <Icon
          src={icon ? icon : icons.profile}
        />
        <PostTitle>{login}</PostTitle>
      </SearchLink>
      <Separator />
      {MemoSlider(src)}
      <Separator />
      <Description>{description}</Description>
    </PostContainer>
  );
};

export default observer(BigPostCard);
