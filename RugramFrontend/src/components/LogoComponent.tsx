import { FC } from "react";
import { icons } from "../enums";
import styled from "styled-components";

const Logo = styled.div`
  position: absolute;
  top: 30px;
  right: 30px;
  img {
    height: 48px;
  }
`;

const LogoComponent: FC = () => {
  return (
    <Logo>
      <img src={icons.logo} />
    </Logo>
  );
};

export default LogoComponent;
