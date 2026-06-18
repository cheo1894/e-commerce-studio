import React, { Suspense, useState } from "react";
import "./index.css";

import { Outlet } from "react-router";

const LoadingFallback = () => <div>Cargando...</div>;

type Props = {};

function Index({}: Props) {
  return (
    <div className="auth-background">
      <div className="auth-body">
        <h1>Welcome to CMS</h1>
        <Suspense fallback={<LoadingFallback />}>
          <Outlet />
        </Suspense>
      </div>
    </div>
  );
}

export default Index;
