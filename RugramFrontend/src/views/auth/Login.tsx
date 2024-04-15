import { createRef, FC, useState } from 'react';
import Title from '../../components/ui/Title';
import { WithValidation } from '../../types';
import Input from '../../components/ui/Input';
import Button from '../../components/ui/Button';
import { FooterContainer, LoginContainer, Separator, StyledLink, TextForm } from './AuthStyledComponents';

const Login : FC = () => {

  const refs = [
    {
      ref: createRef<WithValidation>(),
      id: 'EmailRef',
    },
    {
      ref: createRef<WithValidation>(),
      id: 'PasswordlRef',
    },
  ]

  const [ EmailRef, PasswordRef ] = refs;

  const [email, setEmail] = useState<string>('');
  const [emailError, setEmailError] = useState<string | undefined>('');
  const [password, setPassword] = useState<string>('');
  const [passwordError, setPasswordError] = useState<string | undefined>('');

  const registrationHandler = () => {
    refs.map((ref) => {
      if (ref.id === 'EmailRef') {
        setEmailError(ref.ref.current?.validate())
      }
      else {
        setPasswordError(ref.ref.current?.validate())

      }
    })
    if (!emailError && !passwordError) {
      console.log('Норм')
    }
  }

  return (
    <LoginContainer>
      <Title
        text='Вход'
      />
      <Separator>
        <TextForm>
          <Input
            type='email'
            value={email}
            onChange={(value) => {
              setEmailError('')
              setEmail(value)
            }}
            title={'Email'}
            errorMessage={emailError}
            ref={EmailRef.ref}
          />
          <Input
            type='password'
            value={password}
            onChange={(value) => {
              setPasswordError('')
              setPassword(value)
            }}
            title={'Пароль'}
            errorMessage={passwordError}
            ref={PasswordRef.ref}
          />
          <Button
            onClick={registrationHandler}
            text='Войти'
          />
        </TextForm>
        <FooterContainer>
          <span>Ещё нет аккаунта?</span>
          <StyledLink to='/registration'>Регистрация</StyledLink>
        </FooterContainer>
      </Separator>

    </LoginContainer>
  );
};

export default Login;
