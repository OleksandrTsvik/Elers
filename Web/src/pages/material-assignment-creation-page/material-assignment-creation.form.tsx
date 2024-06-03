import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useCreateCourseMaterialAssignmentMutation } from '../../api/course-materials.mutations.api';
import {
  CourseTabType,
  MaterialAssignmentForm,
  MaterialAssignmentFormValues,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  tabId: string;
  courseTabType: CourseTabType;
}

export default function MaterialAssignmentCreationForm({
  courseId,
  tabId,
  courseTabType,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialAssignmentMutation();

  const handleSubmit = async (values: MaterialAssignmentFormValues) => {
    await createCourseMaterial({ tabId, ...values })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

  return (
    <MaterialAssignmentForm
      initialValues={{ title: '', description: '', maxFiles: 0, maxGrade: 10 }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
