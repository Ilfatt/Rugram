import { FC, useState } from 'react';
import styled from 'styled-components';
import { icons } from '../enums';
import { NavLink } from 'react-router-dom';
import { GlassDiv } from '../styles';
import Icon from './ui/Icon';

const BarContainer = styled(GlassDiv)`
  display: flex;
  flex-direction: column;
  position: sticky;
  padding: 16px 8px;
  justify-content: space-between;
  min-height: 88vh;
  border-radius: 10px;
`

const LinkElem = styled(NavLink)`
  &.active {
    img {
      filter: invert(45%) sepia(16%) saturate(1218%) hue-rotate(204deg) brightness(104%) contrast(95%);
    }
  }
`

const MainButtons = styled.div`
  display: flex;
  flex-direction: column;
  gap: 16px;
`

const Navbar : FC = () => {

  // const [isOpened, setIsOpened] = useState<boolean>(false);

  return (
    <BarContainer>
      <MainButtons>
        <LinkElem
          to={'/recommendation'}
        >
          <Icon icon={icons.browse}/>
        </LinkElem >
        <LinkElem
          to={'/profile'}
        >
          <Icon icon={icons.profile}/>
        </LinkElem >
        <LinkElem
          to={'/plus'}
        >
          <Icon icon={icons.plus}/>
        </LinkElem >
      </MainButtons>
      {/* <Icon
        icon={icons.seeMore}
        onClick={() => setIsOpened(!isOpened)}
      /> */}
    </BarContainer>
  );
};

export default Navbar;
