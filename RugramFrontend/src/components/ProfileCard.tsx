/* eslint-disable @typescript-eslint/no-explicit-any */
import { ChangeEvent, FC, useRef, useState} from 'react';
import styled from 'styled-components';
import { GlassDiv } from '../styles';
import UseStores from '../hooks/useStores';
import { useParams } from 'react-router-dom';
import Button from './ui/Button';
import { icons } from '../enums';
import { observer } from 'mobx-react';

const ProfileCardContainer = styled(GlassDiv)`
  width: 25vw;
  height: 80vh;
  border-radius: 16px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  position: sticky;
`
const ChieldContainer = styled.div`
  width: -webkit-fill-available;
  height: -webkit-fill-available;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding: 36px;
`

const Icon = styled.img`
  /* background-color: red; */
  padding: 4px;
  width: -webkit-fill-available;
  height: -webkit-fill-available;
  width: 150px;
  height: 150px;
  border-radius: 50%;
  border: 3px solid black;

  &:hover {
    cursor: pointer;
    background-color: rgba(45, 47, 53, 0.1);
  }
`

const UpperBlock = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  width: -webkit-fill-available;
  height: -webkit-fill-available;
  gap: 12px;
`;

const Title = styled.div`
  font-size: 24px;
  font-weight: bold;
`

const ProfileCard : FC<{isSameUser: boolean}> = ({isSameUser}) => {
  const { userStore, uploadStore } = UseStores();
  const { id } = useParams();

  const [uploadError, setUploadError] = useState('')
  const uploadRef = useRef<HTMLInputElement>(null)

  const handleUpload = (e: ChangeEvent<HTMLInputElement>) => {
    if (e.target.files === null) {
      return
    }
    const file = e.target.files[0]

    if (file) {
      if (!['image/png', 'image/jpeg', 'image/jpg'].includes(file.type)) {
        setUploadError('Для загрузки доступны только файлы формата .png/.jpeg/.jpg')
        return;
      }

      const fileReader = new FileReader()
      fileReader.onload = (event) => {
        const contents = event?.target?.result
        uploadStore.currentImage = contents as string;
      }

      e.target.value = ''
      fileReader.readAsDataURL(file)
    } else {
      setUploadError('Ошибка загрузки файла. Попробуйте ещё раз.')
    }
  }

  return (
    <ProfileCardContainer>
      <ChieldContainer>
        <UpperBlock>
          <Icon
            onClick={() => {
              setUploadError('');
              uploadRef.current?.click();
            }}
            src={userStore.user.img ? userStore.user.img : icons.profile}
          />
          {uploadError}
          <input
            ref={uploadRef}
            accept='image/*'
            multiple={false}
            onChange={handleUpload}
            style={{ display: 'none' }}
            type="file"
          />

          <Title>{userStore.user.username}</Title>
          <div>
            {`Folovers: ${userStore.user.followersCount}`}
          </div>
        </UpperBlock>

        {!isSameUser && id && (
          <Button
            onClick={() => {
              userStore.follow(id!)
            }}
            text='Подписаться'
          />
        )}
      </ChieldContainer>
    </ProfileCardContainer>
  );
};

export default observer(ProfileCard);