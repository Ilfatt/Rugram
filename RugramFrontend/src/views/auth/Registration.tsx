import { createRef, FC, useState } from 'react';
import Title from '../../components/ui/Title';
import { WithValidation } from '../../types';
import Input from '../../components/ui/Input';
import Button from '../../components/ui/Button';
import { FooterContainer, RegContainer, Separator, StyledLink, TextForm } from './AuthStyledComponents';

const Registration : FC = () => {

  const refs = [
    {
      ref: createRef<WithValidation>(),
      id: 'TextRef',
    },
  ]

  const [ TextRef ] = refs;

  const [email, setEmail] = useState<string>('');
  const [textError, setTextError] = useState<string | undefined>('');

  const registrationHandler = () => {
    refs.map((ref) => {
      setTextError(ref.ref.current?.validate())
    })
    if (!textError) {
      console.log('Норм')
    }
  }

  return (
    <RegContainer>
      <Title
        text='Регистрация'
      />
      <Separator>
        <TextForm>
          <Input
            type='email'
            value={email}
            onChange={(value) => {
              setTextError('')
              setEmail(value)
            }}
            title={'Email'}
            errorMessage={textError}
            ref={TextRef.ref}
          />
          <Button
            onClick={registrationHandler}
            text='Зарегистрироваться'
          />
        </TextForm>
        <FooterContainer>
          <span>Уже есть аккаунт?</span>
          <StyledLink to='/login'>Войти</StyledLink>
        </FooterContainer>
      </Separator>

    </RegContainer>
  );
};

export default Registration;
