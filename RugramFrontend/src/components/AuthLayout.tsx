import { FC } from 'react';
import BackgroundVideo from './BackgroundVideo';
import { Outlet } from 'react-router-dom';
import LogoComponent from './LogoComponent';
import styled from 'styled-components';

const Container = styled.div`
  padding: 36px 48px;
  display: flex;
  flex-direction: row;
  gap: 48px;
`

const TextContainer = styled.div`
  width: 50vw;
  height: 90vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`

const MarketingPhrases = styled.p`
  width: 60%;
  padding: 16px 32px;
  border: 4px black solid;
  display: flex;
  font-size: 28px;
  font-weight: 700;
  &:nth-child(2n) {
    align-self: flex-end;
  }
`

const AuthLayout : FC = () => {
  return (
    <Container>
      <BackgroundVideo/>
      <LogoComponent />
      <Outlet />
      <TextContainer>
        <MarketingPhrases>
          Делитесь кусочком своей жизни со всеми
        </MarketingPhrases>
        <MarketingPhrases>
          Следите за обновлениями друзей
        </MarketingPhrases>
        <MarketingPhrases>
          Узнавайте новые места
        </MarketingPhrases>
        <MarketingPhrases>
          хз что написать ещё
        </MarketingPhrases>
      </TextContainer>

    </Container>
  );
};

export default AuthLayout;
