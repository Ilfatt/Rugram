import { FC, useEffect, useRef } from "react";
import styled from "styled-components";
import Navbar from "./NavBar";
import SearchBar from "./SearchBar";
import { Outlet, useNavigate } from "react-router-dom";
import BackgroundVideo from "./BackgroundVideo";
import LogoComponent from "./LogoComponent";
import UseStores from "../hooks/useStores";
import ModalWindow from "./ModalWindow";
import { observer } from "mobx-react";

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




const PageLayout: FC = () => {

  const jwtUpdateIntervalId = useRef<NodeJS.Timer | null>(null);

  const { userStore, modalStore } = UseStores();
  const navigate = useNavigate();

  const startJwtUpdateInterval = () => {
    jwtUpdateIntervalId.current = setInterval(() => {
      userStore.updateJwt();
    }, 2 * 60 * 1000); // 2 minutes in milliseconds
  };

  const stopJwtUpdateInterval = () => {
    if (jwtUpdateIntervalId.current) {
      // clearInterval(jwtUpdateIntervalId.current);
      jwtUpdateIntervalId.current = null;
    }
  };

  useEffect(() => {
    if (userStore.token) {
      userStore.updateJwt();
      startJwtUpdateInterval();
    }
    return () => {
      stopJwtUpdateInterval()
      modalStore.clearStore();
    };
  }, [])

  useEffect(() => {
    if (!userStore.token) {
      navigate("/auth/login");
    } else if (userStore.token && window.location.pathname === "/") {
      navigate("/recommendation")
    }
  }, [userStore.token]);

  return (
    <PageContainer>
      <BackgroundVideo />
      <LogoComponent />
      <Navbar />
      <PageBody>
        <SearchBar />
        <Outlet />
      </PageBody>
      {modalStore.isOpen && (
        <ModalWindow
          onClose={() => {
            if (modalStore.onClose) {
              modalStore.onCloseModal();
            } else {
              modalStore.closeModal();
            }
          }}
          title={modalStore.title}
        >
          {modalStore.content}
        </ModalWindow>
      )}
    </PageContainer>
  );
};

export default observer(PageLayout);
