import { useState } from 'react';

export default function useModal(initOpen = false) {
  const [isOpen, setIsOpen] = useState(initOpen);

  const onOpen = () => {
    setIsOpen(true);
  };

  const onClose = () => {
    setIsOpen(false);
  };

  const onToggle = () => {
    setIsOpen(!isOpen);
  };

  return { isOpen, onOpen, onClose, onToggle };
}
