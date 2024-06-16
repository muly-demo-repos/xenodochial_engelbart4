import React, { useEffect, useState } from "react";
import { Admin, DataProvider, Resource } from "react-admin";
import buildGraphQLProvider from "./data-provider/graphqlDataProvider";
import { theme } from "./theme/theme";
import Login from "./Login";
import "./App.scss";
import Dashboard from "./pages/Dashboard";
import { BookList } from "./book/BookList";
import { BookCreate } from "./book/BookCreate";
import { BookEdit } from "./book/BookEdit";
import { BookShow } from "./book/BookShow";
import { MemberList } from "./member/MemberList";
import { MemberCreate } from "./member/MemberCreate";
import { MemberEdit } from "./member/MemberEdit";
import { MemberShow } from "./member/MemberShow";
import { AuthorList } from "./author/AuthorList";
import { AuthorCreate } from "./author/AuthorCreate";
import { AuthorEdit } from "./author/AuthorEdit";
import { AuthorShow } from "./author/AuthorShow";
import { LibraryList } from "./library/LibraryList";
import { LibraryCreate } from "./library/LibraryCreate";
import { LibraryEdit } from "./library/LibraryEdit";
import { LibraryShow } from "./library/LibraryShow";
import { jwtAuthProvider } from "./auth-provider/ra-auth-jwt";

const App = (): React.ReactElement => {
  const [dataProvider, setDataProvider] = useState<DataProvider | null>(null);
  useEffect(() => {
    buildGraphQLProvider
      .then((provider: any) => {
        setDataProvider(() => provider);
      })
      .catch((error: any) => {
        console.log(error);
      });
  }, []);
  if (!dataProvider) {
    return <div>Loading</div>;
  }
  return (
    <div className="App">
      <Admin
        title={"MulyNodeJs"}
        dataProvider={dataProvider}
        authProvider={jwtAuthProvider}
        theme={theme}
        dashboard={Dashboard}
        loginPage={Login}
      >
        <Resource
          name="Book"
          list={BookList}
          edit={BookEdit}
          create={BookCreate}
          show={BookShow}
        />
        <Resource
          name="Member"
          list={MemberList}
          edit={MemberEdit}
          create={MemberCreate}
          show={MemberShow}
        />
        <Resource
          name="Author"
          list={AuthorList}
          edit={AuthorEdit}
          create={AuthorCreate}
          show={AuthorShow}
        />
        <Resource
          name="Library"
          list={LibraryList}
          edit={LibraryEdit}
          create={LibraryCreate}
          show={LibraryShow}
        />
      </Admin>
    </div>
  );
};

export default App;
