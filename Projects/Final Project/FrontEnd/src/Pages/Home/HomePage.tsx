export function HomePage() {
  return (
    <>

      {/* Navbar pedorro */}
      <nav className="navbar navbar-expand-lg navbar-light bg-white shadow-sm px-3">
        <a className="navbar-brand fw-bold text-primary" href="#">
           <img
            src="./src/Assets//MediAgenda.png"
            alt="MediAgenda Icon"
            style={{ width: "32px", height: "32px" }}
            className="me-2"
            />
          MediAgenda
        </a>

        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse justify-content-end" id="navbarNav">
          <ul className="navbar-nav">
            <li className="nav-item me-3">
              <a className="btn btn-outline-primary" href="/login">Iniciar Sesión</a>
            </li>
            <li className="nav-item">
              <a className="btn btn-primary" href="/register">Registrarse</a>
            </li>
          </ul>
        </div>
      </nav>

      {/* Este es el cuerpo del home*/}
      <section className="container my-5">
        <div className="row align-items-center">

          <div className="col-md-6 mb-4">
            <h1 className="fw-bold mb-3">
              Gestiona tus <span className="text-primary">citas médicas</span> fácilmente
            </h1>
            <p className="text-muted">
              MediAgenda te ayuda a reservar y administrar tus citas médicas de manera
              rápida y sencilla. Ideal para pacientes, doctores y clínicas.
            </p>

            <a href="/register" className="btn btn-primary btn-lg mt-3">
              Empezar Ahora
            </a>
          </div>

          {/* Imagen en grande */}
          <div className="col-md-6 text-center">
            <img
              src="https://st2.depositphotos.com/3889193/8319/i/450/depositphotos_83195690-stock-photo-doctors-handshaking-at-hospital.jpg"
              alt="Medical illustration"
              className="img-fluid"
              style={{ maxHeight: "350px" }}
            />
          </div>
        </div>
      </section>

      {/* Cartas con las secciones */}
      <section className="container text-center my-5">
        <h2 className="fw-bold mb-4">¿Qué puedes hacer con MediAgenda?</h2>

        <div className="row g-4 justify-content-center">

          <div className="col-10 col-md-4">
            <div className="card p-4 shadow-sm">
              <img
                src="https://cdn-icons-png.flaticon.com/512/3774/3774299.png"
                alt="icono de doctor"
                className="img-fluid mb-3"
                style={{ width: "80px" }}
              />
              <h5 className="fw-bold">Agendar Citas</h5>
              <p className="text-muted">Reserva tus citas de manera rápida y sin complicaciones.</p>
            </div>
          </div>

          <div className="col-10 col-md-4">
            <div className="card p-4 shadow-sm">
              <img
                src="https://cdn-icons-png.flaticon.com/512/3045/3045303.png"
                alt="icono de registros"
                className="img-fluid mb-3"
                style={{ width: "80px" }}
              />
              <h5 className="fw-bold">Citas a medida</h5>
              <p className="text-muted">Podras seleccionar el dia que gustes para tomar tu consulta.</p>
            </div>
          </div>

          <div className="col-10 col-md-4">
            <div className="card p-4 shadow-sm">
              <img
                src="https://cdn-icons-png.flaticon.com/512/3209/3209265.png"
                alt="icon"
                className="img-fluid mb-3"
                style={{ width: "80px" }}
              />
              <h5 className="fw-bold">Historial Médico</h5>
              <p className="text-muted">Consulta rápidamente tu historial de visitas y medicamentos agendados.</p>
            </div>
          </div>

        </div>
      </section>

    </>
  );
}
