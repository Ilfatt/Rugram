import { FC } from 'react';
import styled from 'styled-components';
import Navbar from './NavBar';
import SearchBar from './SearchBar';
import { Outlet } from 'react-router-dom';
import BackgroundVideo from './BackgroundVideo';
import LogoComponent from './LogoComponent';

const PageContainer = styled.div`
  display: flex;
  flex-direction: row;
  gap: 48px;
  margin: 24px 12px;
`;

const PageBody = styled.div`
  display: flex;
  flex-direction: column;
  gap: 24px;
`;

const PageLayout : FC = () => {
  return (
    <PageContainer>
      <BackgroundVideo />
      <LogoComponent />
      <Navbar />
      <PageBody>
        <SearchBar />
        <Outlet />
      </PageBody>
    </PageContainer>
  );
};

export default PageLayout;
