import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useCreateCourseMaterialLinkMutation } from '../../api/course-materials.mutations.api';
import {
  CourseTabType,
  MaterialLinkForm,
  MaterialLinkFormValues,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  tabId: string;
  courseTabType: CourseTabType;
}

export default function MaterialLinkCreationForm({
  courseId,
  tabId,
  courseTabType,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialLinkMutation();

  const handleSubmit = async (values: MaterialLinkFormValues) => {
    await createCourseMaterial({ tabId, ...values })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

  return (
    <MaterialLinkForm
      initialValues={{ title: '', link: '' }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
