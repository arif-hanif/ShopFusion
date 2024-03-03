import { Suspense } from 'react';
import { createGlobalStyle } from 'styled-components';
import { BrowserRouter } from 'react-router-dom';
import { Skeleton } from 'antd';

const GlobalStyle = createGlobalStyle`
  #root,body {
    height: 100vh;
    margin: 0;
  }
`;

export const App = () => (
  <Suspense fallback={<Skeleton />}>
    <GlobalStyle />

    <BrowserRouter>
      <>Store Front - Web!</>
    </BrowserRouter>
  </Suspense>
);
