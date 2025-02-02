document.addEventListener('DOMContentLoaded', function() {
    const apiUrl = 'http://localhost:5278/v1/students';

    function getStudentIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('id');
    }

    async function fetchStudentData(id) {
        try {
            const response = await fetch(`${apiUrl}/${id}`);
            if (!response.ok) {
                throw new Error('Erro ao buscar os dados do estudante');
            }
            const result = await response.json();
            const student = result.data;

            console.log("Dados do estudante:", student);

            document.getElementById('studentId').value = student.id;
            document.getElementById('editName').value = student.name;
            document.getElementById('editEmail').value = student.email;
            document.getElementById('editRegistration').value = student.registration;
            document.getElementById('editDateOfBirth').value = student.dateOfBirth.split('T')[0];
            document.getElementById('editCourseName').value = student.course?.courseName || '';
            document.getElementById('editDuration').value = student.course?.duration || '';
        } catch (error) {
            console.error('Erro ao carregar dados do estudante:', error);
            alert('Erro ao carregar os dados do estudante. Tente novamente mais tarde.');
        }
    }

    document.getElementById('editForm').addEventListener('submit', async function(event) {
        event.preventDefault();

        const studentId = document.getElementById('studentId').value;

        if (!studentId) {
            alert('ID do estudante não encontrado.');
            return;
        }

        const studentData = {
            id: parseInt(studentId),
            name: document.getElementById('editName').value,
            email: document.getElementById('editEmail').value,
            registration: document.getElementById('editRegistration').value,
            dateOfBirth: document.getElementById('editDateOfBirth').value,
            course: {
                courseName: document.getElementById('editCourseName').value,
                duration: parseInt(document.getElementById('editDuration').value)
            }
        };

        console.log('Dados do estudante a serem atualizados:', studentData);

        if (!studentData.name || !studentData.email || !studentData.registration || !studentData.dateOfBirth || !studentData.course.courseName) {
            alert('Por favor, preencha todos os campos obrigatórios.');
            return;
        }

        try {
            const response = await fetch(`${apiUrl}/${studentId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(studentData)
            });

            if (!response.ok) {
                throw new Error('Erro ao atualizar estudante');
            }

            alert('Estudante atualizado com sucesso!');
            window.location.href = 'index.html';
        } catch (error) {
            console.error('Erro ao atualizar estudante:', error);
            alert('Erro ao atualizar estudante. Tente novamente.');
        }
    });

    const studentId = getStudentIdFromUrl();
    if (studentId) {
        fetchStudentData(studentId);
    } else {
        alert('ID do estudante não encontrado na URL.');
        window.location.href = 'index.html';
    }
});
