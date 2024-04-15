import { FC, useState } from 'react';
import styled from 'styled-components';
import { icons } from '../enums';
import LogoComponent from './LogoComponent';
import { GlassDiv } from '../styles';

const SearchContainer = styled.div`
  min-width: 88vw;
  display: flex;
  justify-content: center;
  position: sticky;
`

const BarContainer = styled(GlassDiv)`
  display: flex;
  width: 40vw;
  padding: 6px 12px;
  align-items: center;
  border-radius: 12px;
  gap: 12px;
  img {
    width: 24px;
    height: 24px;
  }
`

const StyledInput = styled.input`
  background-color: inherit;
  border: none;
  width: 90%;
  padding: 8px;
  font-size: 16px;
  &:focus
  {
    outline: none;
  }
`

const SearchBar : FC = () => {
  const [search, setSearch] = useState('');
  return (
    <SearchContainer>
      <BarContainer>
        <img src={icons.search} />
        <StyledInput
          type="text"
          placeholder='Начните вводить логин'
          value={search}
          onChange={(value) => setSearch(value.target.value)}
        />
      </BarContainer>
    </SearchContainer>
  );
};

export default SearchBar;
