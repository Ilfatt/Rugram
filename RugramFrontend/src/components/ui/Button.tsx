import { FC } from 'react';
import styled from 'styled-components';

interface ButtonProps {
  text: string;
  onClick: () => void;
}

const StyledButton = styled.button`
  border-radius: 8px;
  padding: 16px;
  font-size: 16px;
  font-weight: 600;
  width: -webkit-fill-available;

  cursor: pointer;

  background: linear-gradient(135deg, rgba(255, 255, 255, 0.1), rgba(255, 255, 255, 0));
  backdrop-filter: blur(10px);
  -webkit-backdrop-filter: blur(10px);
  border: 2px solid black;;
  box-shadow: 0 8px 32px 0 rgba(0, 0, 0, 0.37);
`

const Button : FC<ButtonProps> = ({onClick, text}) => {
  return (
    <StyledButton
      onClick={onClick}
    >
      {text}
    </StyledButton>
  );
};

export default Button;
