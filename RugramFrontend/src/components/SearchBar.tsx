import { FC, useEffect, useMemo, useState } from "react";
import styled from "styled-components";
import { icons } from "../enums";
import { GlassDiv } from "../styles";
import useDebounce from "../hooks/useDebounce";
import UseStores from "../hooks/useStores";

const SearchContainer = styled.div`
  min-width: 88vw;
  display: flex;
  justify-content: center;
  position: fixed;
  top: 20px;
  z-index: 999;
  align-self: center;
`;

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
  };
`;

const StyledInput = styled.input`
  background-color: inherit;
  border: none;
  width: 90%;
  padding: 8px;
  font-size: 16px;
  &:focus {
    outline: none;
  };
`;

const SearchResult = styled(GlassDiv)`
  display: flex;
  position: absolute;
  flex-direction: column;
  gap: 8px;
  padding: 6px 12px;
  border-radius: 12px;
  z-index: 999;
  top: 64px;
  width: 40vw;
  align-items: center;
`;

const SearchBar: FC = () => {
  const { userStore } = UseStores();
  const [search, setSearch] = useState("");
  const debounceSearch = useDebounce(search, 1000);

  useEffect(() => {
    if (search) {
      userStore.search(search);
    }
  }, [debounceSearch]);

  const result = useMemo(() => {
    return userStore.searchProfiles?.profiles.length
      ? userStore.searchProfiles?.profiles.map(
        (profile) => (
          <div key={profile.id}>{profile.profileName}</div>
        )
      ) : (
        <div>No results</div>
      )
  }, [userStore.searchProfiles?.profiles])

  return (
    <SearchContainer>
      <BarContainer>
        <img src={icons.search} />
        <StyledInput
          onChange={(value) => setSearch(value.target.value)}
          placeholder="Начните вводить логин"
          type="text"
          value={search}
        />
      </BarContainer>
      {search.length && (
        <SearchResult>
          {result}
        </SearchResult>

      )}
    </SearchContainer>
  );
};

export default SearchBar;
