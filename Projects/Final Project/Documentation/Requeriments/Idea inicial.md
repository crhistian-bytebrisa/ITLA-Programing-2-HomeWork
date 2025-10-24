Una app que se encargue de manejar las citas de los pacientes además de las consultas de los mismos.

# 🩺Requisitos Funcionales

## 1. Gestión de pacientes

- **RF1:** El paciente debe poder registrarse con su **cédula, nombre completo, número telefónico, correo, fecha de nacimiento, seguro médico y género**.  
- **RF2:** En caso de ser **menor de edad**, el registro deberá vincularse a un **tutor legal** y guardar sus datos de referencia.  
- **RF3:** El paciente debe poder **modificar ciertos datos personales**, como su número telefónico, seguro médico o correo.  
- **RF4:** El sistema debe **mostrar el historial clínico del paciente**, incluyendo consultas anteriores, recetas, referimientos y copias digitales de los documentos emitidos.  
- **RF5:** Los pacientes tendrán datos médicos básicos ingresados por el médico, tales como **alergias, enfermedades y tipo de sangre**.  
- **RF6:** El sistema debe permitir **registrar los medicamentos actuales del paciente**, actualizados por el médico en cada consulta.  

---

## 2. Gestión de citas

- **RF7:** El paciente debe poder **agendar consultas** en los días y horarios disponibles del médico, visualizando cuántos cupos quedan.  
- **RF8:** El paciente debe poder **cancelar o reprogramar citas** según su conveniencia.  
- **RF9:** El sistema debe **emitir un recordatorio automático** al paciente al menos un día antes de la visita programada.  

---

## 3. Gestión médica

- **RF10:** El médico debe poder **registrar notas clínicas** sobre cada paciente y revisar el historial completo de sus consultas.  
- **RF11:** El médico debe poder **definir las clínicas** donde ofrece consultas, incluyendo horarios y márgenes para emergencias o consultas sin cita previa.  
- **RF12:** El médico debe poder **atender distintos tipos de consulta** (consulta general, seguimiento, analítica, referimiento, entre otros).  
- **RF13:** El médico debe poder **subir archivos PDF u otros documentos** con los resultados clínicos del paciente.  
- **RF14:** El sistema debe permitir **transferir el historial clínico completo del paciente** a otro médico en caso de referimiento.  
---  

---
# 🖥️Requisitos no funcionales

## 1. Seguridad

- **RNF1:** Los **datos personales y médicos** de los pacientes deben almacenarse **cifrados tanto en tránsito como en almacenamiento** (aplicado a la mayoría de los campos sensibles).  
- **RNF2:** Se debe implementar un **método de autenticación seguro** para cada inicio de sesión (tengo en mente JWT).  
- **RNF3:** Todos los accesos dentro de la aplicación deben estar **controlados por roles** (paciente, médico, administrador).  
- **RNF4:** Los **datos de los pacientes** solo deben ser visibles por **ellos mismos y su médico asignado**.  
- **RNF5:** En el caso de menores de edad, los datos solo pueden ser vistos por su **tutor legal y el médico**.  
- **RNF6:** Todo **acceso o modificación** a la información médica debe quedar **registrado en un log de auditoría** inalterable *(Una tabla en la base de datos)*.  
- **RNF7:** El médico deberá acceder mediante un **método de verificación de dos pasos (2FA)**. *(Este lo implementare si me da el tiempo)*  

---

## 2. Rendimiento y disponibilidad

- **RNF8:** El **tiempo máximo de respuesta** del sistema ante cualquier solicitud no debe superar los **5 segundos** bajo condiciones normales.  
- **RNF9:** Deben realizarse **copias de seguridad automáticas** de la base de datos de forma frecuente para garantizar la integridad y disponibilidad de la información *(Esto es lo ideal, pero no se como implementarlo para las migraciones con Entity Framework)*.  

---

## 3. Usabilidad y accesibilidad

- **RNF10:** La **interfaz del sistema** debe ser **intuitiva y fácil de usar** para todo tipo de usuario, priorizando claridad visual y flujo natural de navegación.  
- **RNF11:** La aplicación debe ser **responsive**, adaptándose correctamente a distintos dispositivos (móvil, tableta y escritorio).  

---
# ⚖️ Leyes

Investigar las siguientes leyes para que sea un software lo mas legal posible y cumpla con varios de los requerimientos antes mencionados.

Ley NO.42-01
Ley NO.53-07
Ley NO.87-01
Ley NO.87-01
Ley NO.126-02
Ley NO.136-03
Ley NO.172-13
Ley NO.385-05