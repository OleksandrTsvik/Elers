import {
  selectCourseEditState,
  setActiveCourseTab,
  setModalMode,
} from './course-edit.slice';
import { useAppDispatch, useAppSelector } from '../../hooks/redux-hooks';

export default function useCourseEditState() {
  const appDispatch = useAppDispatch();

  const { activeCourseTab, modalMode } = useAppSelector(selectCourseEditState);

  const onCloseModal = () => {
    appDispatch(setActiveCourseTab(undefined));
    appDispatch(setModalMode(undefined));
  };

  return { activeCourseTab, modalMode, onCloseModal };
}
