const apiUrl = 'http://localhost:5278/v1/students';

document.getElementById('studentForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const studentId = document.getElementById('studentId').value;
    const studentData = {
        name: document.getElementById('name').value,
        registration: document.getElementById('registration').value,
        email: document.getElementById('email').value,
        dateOfBirth: new Date(document.getElementById('dateOfBirth').value).toISOString(),
        course: {
            courseName: document.getElementById('courseName').value,
            duration: parseInt(document.getElementById('duration').value)
        }
    };

    try {
        if (studentId) {
            const response = await fetch(`${apiUrl}/${studentId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(studentData)
            });
            if (!response.ok) throw new Error('Erro ao editar estudante');
        } else {
            const response = await fetch(apiUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(studentData)
            });
            if (!response.ok) throw new Error('Erro ao criar estudante');
        }

        document.getElementById('studentForm').reset();
        document.getElementById('studentId').value = '';
        fetchStudents();
    } catch (error) {
        console.error('Erro ao salvar estudante:', error);
        alert(error.message || 'Erro ao salvar estudante. Tente novamente.');
    }
});

async function fetchStudents(page = 1) {
    const response = await fetch(`${apiUrl}?pageNumber=${page}&pageSize=25`);
    const result = await response.json();
    const students = result.data;
    const tableBody = document.getElementById('studentTableBody');
    tableBody.innerHTML = '';

    students.forEach(student => {
        const row = `<tr>
            <td>${student.id}</td>
            <td>${student.name}</td>
            <td>${student.registration}</td>
            <td>${student.email}</td>
            <td>${new Date(student.dateOfBirth).toLocaleDateString()}</td>
            <td>${student.course.courseName} (${student.course.duration} meses)</td>
            <td class="actions">
                <button onclick="window.location.href='edit-student.html?id=${student.id}'">Editar</button>
                <button onclick="deleteStudent('${student.id}')">Excluir</button>
            </td>
        </tr>`;
        tableBody.innerHTML += row;
    });
}

async function deleteStudent(id) {
    const confirmDelete = confirm('Tem certeza que deseja excluir este estudante?');
    if (!confirmDelete) return;

    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

        if (!response.ok) {
            throw new Error('Erro ao excluir estudante');
        }

        fetchStudents();
    } catch (error) {
        console.error('Erro ao excluir estudante:', error);
        alert(error.message || 'Erro ao excluir estudante. Tente novamente.');
    }
}

fetchStudents();
